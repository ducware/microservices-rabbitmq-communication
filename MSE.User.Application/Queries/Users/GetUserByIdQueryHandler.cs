using AutoMapper;
using MSE.User.Domain.Common.Queries;
using MSE.User.Domain.Models;
using MSE.User.Infrastructure.Repositories;

namespace MSE.User.Application.Queries.Users
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserInfoDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await _userRepository.GetByIdAsync(request.Id);

            var data = _mapper.Map<UserInfoDto>(userInfo);

            return data;
        }
    }
}
