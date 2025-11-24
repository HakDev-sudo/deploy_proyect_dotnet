using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Queries;

public record GetScheduleByIdQuery(Guid Id) : IRequest<ScheduleReadDto?>;

internal sealed class GetScheduleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetScheduleByIdQuery, ScheduleReadDto?>
{
    public async Task<ScheduleReadDto?> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        // Get schedule by ID from generic repository (using Guid)
        var schedule = await unitOfWork.Repository<Schedule>().GetByIdAsync(request.Id);

        if (schedule == null)
            return null;

        // mapping
        return mapper.Map<ScheduleReadDto>(schedule);
    }
}