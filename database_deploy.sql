-- =====================================================
-- ARCHERY ACADEMY - DATABASE DEPLOYMENT SCRIPT
-- PostgreSQL 14+
-- Generated: 2025-12-03
-- =====================================================

-- Habilitar extensión UUID
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- =====================================================
-- TABLAS DE CATÁLOGOS
-- =====================================================

-- Booking Statuses (Estados de reserva)
CREATE TABLE booking_statuses (
    id SERIAL PRIMARY KEY,
    code VARCHAR(20) NOT NULL UNIQUE,
    name VARCHAR(50) NOT NULL,
    description TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Payment Methods (Métodos de pago)
CREATE TABLE payment_methods (
    id SERIAL PRIMARY KEY,
    code VARCHAR(30) NOT NULL UNIQUE,
    name VARCHAR(50) NOT NULL,
    description TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Payment Statuses (Estados de pago)
CREATE TABLE payment_statuses (
    id SERIAL PRIMARY KEY,
    code VARCHAR(20) NOT NULL UNIQUE,
    name VARCHAR(50) NOT NULL,
    description TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- TABLAS PRINCIPALES
-- =====================================================

-- Roles
CREATE TABLE roles (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(50) NOT NULL UNIQUE,
    description VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Plans (Planes)
CREATE TABLE plans (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL,
    num_classes INTEGER,
    duration_days INTEGER,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Users (Usuarios)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    status VARCHAR(1) NOT NULL DEFAULT 'A',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    instructor_id UUID,
    CONSTRAINT fk_instructor_user FOREIGN KEY (instructor_id) REFERENCES users(id) ON DELETE SET NULL
);

CREATE INDEX idx_users_instructor ON users(instructor_id);

-- User Roles (Roles de usuario)
CREATE TABLE user_roles (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL,
    role_id UUID NOT NULL,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_user_role_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_user_role_role FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE RESTRICT,
    CONSTRAINT uk_user_role UNIQUE (user_id, role_id)
);

CREATE INDEX idx_user_roles_role ON user_roles(role_id);

-- Schedules (Horarios)
CREATE TABLE schedules (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    instructor_id UUID NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    max_students INTEGER,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_instructor_schedule FOREIGN KEY (instructor_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE INDEX idx_schedules_instructor ON schedules(instructor_id);

-- User Plans (Planes de usuario)
CREATE TABLE user_plans (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL,
    plan_id UUID NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    remaining_classes INTEGER,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_user_plan FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_plan_user FOREIGN KEY (plan_id) REFERENCES plans(id) ON DELETE RESTRICT
);

CREATE INDEX idx_user_plans_user ON user_plans(user_id);
CREATE INDEX idx_user_plans_plan ON user_plans(plan_id);

-- Bookings (Reservas)
CREATE TABLE bookings (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL,
    schedule_id UUID NOT NULL,
    user_plan_id UUID NOT NULL,
    status_id INTEGER NOT NULL,
    payment_status_id INTEGER NOT NULL,
    attended_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_user_booking FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_schedule_booking FOREIGN KEY (schedule_id) REFERENCES schedules(id) ON DELETE CASCADE,
    CONSTRAINT fk_user_plan_booking FOREIGN KEY (user_plan_id) REFERENCES user_plans(id) ON DELETE CASCADE,
    CONSTRAINT fk_booking_status FOREIGN KEY (status_id) REFERENCES booking_statuses(id) ON DELETE RESTRICT,
    CONSTRAINT fk_booking_payment_status FOREIGN KEY (payment_status_id) REFERENCES payment_statuses(id) ON DELETE RESTRICT
);

CREATE INDEX idx_bookings_user ON bookings(user_id);
CREATE INDEX idx_bookings_schedule ON bookings(schedule_id);
CREATE INDEX idx_bookings_user_plan ON bookings(user_plan_id);
CREATE INDEX idx_bookings_status ON bookings(status_id);
CREATE INDEX idx_bookings_payment_status ON bookings(payment_status_id);

-- Payments (Pagos)
CREATE TABLE payments (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    booking_id UUID NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    method_id INTEGER NOT NULL,
    status_id INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_booking_payment FOREIGN KEY (booking_id) REFERENCES bookings(id) ON DELETE RESTRICT,
    CONSTRAINT fk_payment_method FOREIGN KEY (method_id) REFERENCES payment_methods(id) ON DELETE RESTRICT,
    CONSTRAINT fk_payment_status FOREIGN KEY (status_id) REFERENCES payment_statuses(id) ON DELETE RESTRICT
);

CREATE INDEX idx_payments_booking ON payments(booking_id);
CREATE INDEX idx_payments_method ON payments(method_id);
CREATE INDEX idx_payments_status ON payments(status_id);

-- Certificates (Certificados)
CREATE TABLE certificates (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL,
    type VARCHAR(50) NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    verification_code VARCHAR(20) NOT NULL UNIQUE,
    pdf_url TEXT,
    blob_file_name VARCHAR(255),
    issued_at TIMESTAMP NOT NULL,
    issued_by_id UUID,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_certificate_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_certificate_issued_by FOREIGN KEY (issued_by_id) REFERENCES users(id) ON DELETE SET NULL
);

CREATE INDEX idx_certificates_user ON certificates(user_id);
CREATE INDEX idx_certificates_issued_by ON certificates(issued_by_id);

-- Google Tokens (Tokens de Google Calendar)
CREATE TABLE google_tokens (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL,
    access_token TEXT NOT NULL,
    refresh_token TEXT,
    token_type VARCHAR(50),
    expires_in_seconds BIGINT,
    issued_utc TIMESTAMP NOT NULL,
    scope TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    CONSTRAINT fk_google_tokens_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE INDEX idx_google_tokens_user ON google_tokens(user_id);

-- =====================================================
-- DATOS INICIALES (SEED DATA)
-- =====================================================

-- Roles por defecto
INSERT INTO roles (name, description) VALUES
    ('admin', 'Administrador del sistema'),
    ('instructor', 'Instructor de tiro con arco'),
    ('student', 'Estudiante/Alumno');

-- Estados de reserva
INSERT INTO booking_statuses (code, name, description, display_order) VALUES
    ('PENDING', 'Pendiente', 'Reserva pendiente de confirmación', 1),
    ('CONFIRMED', 'Confirmada', 'Reserva confirmada', 2),
    ('CANCELLED', 'Cancelada', 'Reserva cancelada', 3),
    ('COMPLETED', 'Completada', 'Clase completada', 4),
    ('NO_SHOW', 'No asistió', 'El alumno no asistió', 5);

-- Estados de pago
INSERT INTO payment_statuses (code, name, description, display_order) VALUES
    ('PENDING', 'Pendiente', 'Pago pendiente', 1),
    ('PAID', 'Pagado', 'Pago completado', 2),
    ('FAILED', 'Fallido', 'Pago fallido', 3),
    ('REFUNDED', 'Reembolsado', 'Pago reembolsado', 4);

-- Métodos de pago
INSERT INTO payment_methods (code, name, description, display_order) VALUES
    ('CASH', 'Efectivo', 'Pago en efectivo', 1),
    ('CARD', 'Tarjeta', 'Pago con tarjeta de crédito/débito', 2),
    ('TRANSFER', 'Transferencia', 'Transferencia bancaria', 3),
    ('YAPE', 'Yape', 'Pago con Yape', 4),
    ('PLIN', 'Plin', 'Pago con Plin', 5);

-- =====================================================
-- FIN DEL SCRIPT
-- =====================================================
