using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ISettingsRepository _settingsRepository;
        public UserRepository(IMongoContext context, ISettingsRepository settingsRepository) : base(context)
        {
            _settingsRepository = settingsRepository;
        }

        public bool UserControl(string username, string password)
        {
            try
            {
                var salt = Salt();
                var sonuc = this.Filter(x =>
                                            x.Username == username &&
                                            x.Password ==
                                            Hash.PaswordHash(username + salt + password));
                if (sonuc.Count() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        public User UsersControl(string username, string password)
        {
            try
            {
                var salt = Salt();
                var sonuc = this.Filter(x =>
                                            x.Username == username &&
                                            x.Password ==
                                            Hash.PaswordHash(username + salt + password));
                if (sonuc.Count() > 0)
                {
                    return sonuc.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        public bool AddUser(string username, string password)
        {
            try
            {
                var salt = Salt();
                var user = new User
                {
                    Username = username,
                    Password = Hash.PaswordHash(username + salt + password)
                };
                Add(user);
                return true;
            }
            catch (Exception e)
            {
                return false;

            }


        }
        private string Salt()
        {
            var _settingName = "_ProductSalt_";
            var setting = _settingsRepository.Filter(x => x.Name == _settingName).FirstOrDefault();
            if (setting == null)
            {
                setting = new Settings
                {
                    Id = Guid.NewGuid(),
                    Name = _settingName,
                    Value = "_ATB_Salt_Value"
                };
                _settingsRepository.Add(setting);
            }
            return setting.Value;
        }

    }


}
