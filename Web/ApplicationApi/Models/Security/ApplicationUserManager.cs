using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Configuration;
using System;
using BioInfo.Web.ApplicationApi.Models.Security.BindingModels;
using BioInfo.Web.ApplicationApi.Models.Security.Managers;

namespace BioInfo.Web.ApplicationApi.Models.Security
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private bool _disposed;
        private readonly SecurityDBContext _dbContext;

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            var userManager = store as UserStore<ApplicationUser>;
            if (userManager != null)
            {
                _dbContext = userManager.Context as SecurityDBContext;
            }

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<SecurityDBContext>()));
            ConfigureValidationLogicForUserNames(manager);
            ConfigureValidationLogicForPasswords(manager);

            //String emailAccount = ConfigurationManager.AppSettings["emailAccount"];
            //String emailPassword = ConfigurationManager.AppSettings["emailPassword"];
            //String fromAddress = ConfigurationManager.AppSettings["fromAddress"];

            //manager.EmailService = new SendGridEmailService(emailAccount, emailPassword, fromAddress);

            //Guid cDyneLicenseKey = Guid.Parse(ConfigurationManager.AppSettings["cDyneLicenseKey"]);
            //manager.SmsService = new CDyneSMSService(cDyneLicenseKey);

            AddDataProtectionTokens(options, manager);
            return manager;
        }


        public IList<ApplicationUserBindingModel> GetAll()
        {
            var users = from au in _dbContext.Users
                        select new ApplicationUserBindingModel
                        {
                            Id = au.Id,
                            FirstName = au.FirstName,
                            LastName = au.LastName,
                            PhoneNumber = au.PhoneNumber
                        };

            return users.ToList();
        }

        public Task<IQueryable<ApplicationUserBindingModel>> GetAllAsync()
        {
            var users = (from au in _dbContext.Users
                         select new ApplicationUserBindingModel
                         {
                             Id = au.Id,
                             Email = au.Email,
                             FirstName = au.FirstName,
                             LastName = au.LastName,
                             PhoneNumber = au.PhoneNumber
                         });

            return Task.FromResult(users);
        }

        public IList<ApplicationUserBindingModel> GetAll(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var users = _dbContext.Users.Where(predicate).Select(au => new ApplicationUserBindingModel
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                PhoneNumber = au.FirstName
            });

            return users.ToList();
        }

        public Task<IQueryable<ApplicationUserBindingModel>> GetAllAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var users = _dbContext.Users.Where(predicate).Select(au => new ApplicationUserBindingModel
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                PhoneNumber = au.FirstName
            });

            return Task.FromResult(users);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUserBindingModel model)
        {
            var user = await FindByIdAsync(model.Id);
            if (user == null)
            {
                return new IdentityResult("User does not exist");
            }

            if (user.Email != model.Email)
            {
                return new IdentityResult("Changing a user's email is not support at this time");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            return await UpdateAsync(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        internal static void AddDataProtectionTokens(IdentityFactoryOptions<ApplicationUserManager> options, ApplicationUserManager manager)
        {
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }

        internal static void ConfigureValidationLogicForPasswords(ApplicationUserManager manager)
        {
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
        }

        internal static void ConfigureValidationLogicForUserNames(ApplicationUserManager manager)
        {
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }
    }

}