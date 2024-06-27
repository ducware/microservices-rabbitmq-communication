using AutoMapper;
using MSE.Common.Models;
using MSE.User.Application.Services;
using MSE.User.Domain.Common.Commands;
using MSE.User.Domain.Common.Exceptions;
using MSE.User.Domain.Entities;
using MSE.User.Domain.Models;
using MSE.User.Infrastructure.Repositories;
using MSE.User.Infrastructure.Repositories.Interfaces;

namespace MSE.User.Application.Commands.UserAccounts
{
    public class CreateUserAccountCommandHandler : ICommandHandler<CreateUserAccountCommand, BaseCommandResponse>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public CreateUserAccountCommandHandler(IUserAccountRepository userAccountRepository, IUserRepository userRepository, IMapper mapper, IProducerService producerService)
        {
            _userAccountRepository = userAccountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _producerService = producerService;
        }
        public async Task<BaseCommandResponse> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new BusinessRuleException("user_id_is_not_existed", "user_id_is_not_existed");
            }

            var userAccount = _mapper.Map<UserAccount>(request);

            await _userAccountRepository.AddAsync(userAccount);
            await _userAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var data = _mapper.Map<UserAccountDto>(userAccount);
            await _producerService.SendAsync(queueName: nameof(UserAccount), data: data);

            return new BaseCommandResponse("success", "New user account is created!");
        }
    }
}
