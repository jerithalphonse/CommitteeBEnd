using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        bool UpdatePassword(int id, string oldpassword, string newpassword);
        bool ResetPasswordForId(int id, string newpassword);
        IEnumerable<User> GetAll();
        List<User> GetUsersByWilayatId(string id);
        List<User> GetUsersByGovernorateId(string id);
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        List<User> GetUsersByPollingStationId(int id);
        List<User> GetUsersByWilayatCodenRoleId(string code, int id);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool UpdatePassword(int id, string oldpassword, string newpassword)
        {
            var user =  _context.Users.SingleOrDefault(x => x.Id == id);
            if (!VerifyPasswordHash(oldpassword, user.PasswordHash, user.PasswordSalt))
                return false;


            if (!string.IsNullOrWhiteSpace(newpassword))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newpassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            user.PasswordChanged = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }
        public bool ResetPasswordForId(int id, string newpassword)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == id.ToString());
            if (!string.IsNullOrWhiteSpace(newpassword))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newpassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            } 
            user.PasswordChanged = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.Include(values => values.Roles).Include(values => values.PollingStation).Include(values => values.Governorate).Include(values => values.Kiosks).Include(values => values.Wilayat).SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Include(values => values.Roles).Include(values => values.PollingStation).Include(values => values.Governorate).Include(values => values.Kiosks).Include(values => values.Wilayat).SingleOrDefault(x => x.Id == id);
        }

        public List<User> GetUsersByPollingStationId(int id)
        {
            return _context.Users.Include(values => values.Roles).Include(values => values.PollingStation).Include(values => values.Governorate).Include(values => values.Kiosks).Include(values => values.Wilayat).Where(values => values.PollingStationId == id).ToList();
        }

        public List<User> GetUsersByWilayatCodenRoleId(string code, int id)
        {
            return _context.Users.Include(values => values.Roles).Include(values => values.PollingStation).Include(values => values.Governorate).Include(values => values.Kiosks).Include(values => values.Wilayat).Where(values => values.WilayatCode == code && values.RoleId == id).ToList();
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(user.NameEnglish))
                throw new AppException("Name in english is required");
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new AppException("Email is required");
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            if (string.IsNullOrWhiteSpace(user.Gender))
                throw new AppException("Gender is required");
            if (string.IsNullOrWhiteSpace(user.GovernorateCode))
                throw new AppException("Governance is required");
            if (string.IsNullOrEmpty(user.WilayatCode))
                throw new AppException("Wilayat is required");
            if (user.RoleId < 0)
                throw new AppException("Role Id is required");
            if (string.IsNullOrWhiteSpace(user.Phone))
                throw new AppException("Phone is required");
            if (_context.Users.Any(x => x.Phone == user.Phone))
                throw new AppException("Phone \"" + user.Username + "\" is already given to other user");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            Console.Write(user);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.NameEnglish = userParam.NameEnglish;
            user.NameArabic = userParam.NameArabic;
            user.Username = userParam.Username;
            user.Phone = userParam.Phone;
            user.PollingStationId = userParam.PollingStationId;
            user.WilayatCode = userParam.WilayatCode;
            user.KioskId = userParam.KioskId;
            user.GovernorateCode = userParam.GovernorateCode;
            user.ImageUrl = userParam.ImageUrl;
            user.RoleId = userParam.RoleId;
            user.Email = userParam.Email;
            user.AttendedAt = userParam.AttendedAt;
            user.PasswordChanged = user.PasswordChanged;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public List<User> GetUsersByWilayatId(string code)
        {
            return _context.Users.Include(values => values.Roles).Include(values => values.Kiosks).Include(values => values.PollingStation).Include(values => values.Wilayat).Where(values => values.WilayatCode == code).ToList();
        }
        public List<User> GetUsersByGovernorateId(string code)
        {
            return _context.Users.Include(values => values.Roles).Include(values => values.Kiosks).Include(values => values.PollingStation).Include(values => values.Wilayat).Where(values => values.GovernorateCode == code).ToList();
        }
    }
}