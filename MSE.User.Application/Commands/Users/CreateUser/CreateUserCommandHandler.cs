using AutoMapper;
using MSE.User.Domain.Common.Commands;
using MSE.User.Domain.Models;
using MSE.User.Infrastructure.Repositories;

namespace MSE.User.Application.Commands.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(request);

            await _userRepository.AddAsync(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseCommandResponse("success", "New user added successfully!");
        }
    }
}
