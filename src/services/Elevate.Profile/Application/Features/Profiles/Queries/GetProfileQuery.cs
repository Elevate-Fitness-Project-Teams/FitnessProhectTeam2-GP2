using Elevate.Profile.Application.Features.Profile.DTO;
using MediatR;

namespace Elevate.Profile.Application.Features.Profile.Queries
{
    public record GetProfileQuery : IRequest<GetProfileDTo>;
   
}
