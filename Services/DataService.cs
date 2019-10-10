using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IGovernoratesService
    {
        IEnumerable<Governorates> GetAll();
        Governorates GetById(int id);
        Governorates Create(Governorates governorates);
        void Update(Governorates governorates);
        void Delete(int id);
    }


    public class GovernoratesService : IGovernoratesService
    {
        private DataContext _context;

        public GovernoratesService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Governorates> GetAll()
        {
            return _context.Governorates.OrderBy(values => values.SortOrder);
        }

        public Governorates GetById(int id)
        {
            return _context.Governorates.Find(id);
        }

        public Governorates Create(Governorates governorates)
        {
            _context.Governorates.Add(governorates);
            _context.SaveChanges();
            return governorates;
        }

        public void Update(Governorates governoratesParam)
        {
            var governorates = _context.Governorates.Find(governoratesParam.Code);

            if (governorates == null)
                throw new AppException("User not found");


            // update user properties
            governorates.ArabicName = governoratesParam.ArabicName;
            governorates.Code = governoratesParam.Code;
            governorates.Name = governoratesParam.Name;
            governorates.SortOrder = governoratesParam.SortOrder;
            _context.Governorates.Update(governorates);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var governorates = _context.Governorates.Find(id);
            if (governorates != null)
            {
                _context.Governorates.Remove(governorates);
                _context.SaveChanges();
            }
        }
    }

    public interface IWilayatService
    {
        IEnumerable<Wilayats> GetAll();
        IEnumerable<Wilayats> GetByCode(string code);
        Wilayats GetById(int id);
        List<Wilayats> GetWilayatsByGovernorateId(string id);
        Wilayats Create(Wilayats wilayats);
        void Update(Wilayats wilayats);
        void Delete(int id);
    }

    public class WilayatService : IWilayatService
    {
        private DataContext _context;

        public WilayatService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Wilayats> GetAll()
        {
            return _context.Wilayats.Include(values => values.Governorate).OrderBy(values => values.SortOrder);
        }

        public Wilayats GetById(int id)
        {
            return _context.Wilayats.Find(id);
        }
        public IEnumerable<Wilayats> GetByCode(string code)
        {
            return _context.Wilayats.Where(values => values.Code == code).OrderBy(values => values.SortOrder);
        }

        public List<Wilayats> GetWilayatsByGovernorateId(string id)
        {
            return _context.Wilayats.Where(values => values.GovernorateCode == id).OrderBy(values => values.SortOrder).ToList();
        }


        public Wilayats Create(Wilayats wilayats)
        {
            _context.Wilayats.Add(wilayats);
            _context.SaveChanges();
            return wilayats;
        }

        public void Update(Wilayats wilayatParam)
        {
            var wilayats = _context.Wilayats.Find(wilayatParam.Code);

            if (wilayats == null)
                throw new AppException("User not found");


            // update user properties
            wilayats.ArabicName = wilayatParam.ArabicName;
            wilayats.Code = wilayatParam.Code;
            wilayats.Name = wilayatParam.Name;
            wilayats.Governorate = wilayatParam.Governorate;
            _context.Wilayats.Update(wilayats);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var wilayats = _context.Wilayats.Find(id);
            if (wilayats != null)
            {
                _context.Wilayats.Remove(wilayats);
                _context.SaveChanges();
            }
        }
    }

    public interface IPollingStationService
    {
        IEnumerable<PollingStations> GetAll();
        PollingStations GetById(int id);
        List<PollingStations> GetPollingStationsByWilayatsId(string id);
        PollingStations Create(PollingStations wilayats);
        void Update(PollingStations wilayats);
        void Delete(int id);
    }

    public class PollingStationService : IPollingStationService
    {
        private DataContext _context;

        public PollingStationService(DataContext context)
        {
            _context = context;
        }
        public List<PollingStations> GetPollingStationsByWilayatsId(string id)
        {
            return _context.PollingStations.Where(values => values.WilayatCode == id).ToList();
        }

        public IEnumerable<PollingStations> GetAll()
        {
            return _context.PollingStations.Include(values => values.Wilayat);
        }

        public PollingStations GetById(int id)
        {
            return _context.PollingStations.Find(id);
        }

        public PollingStations Create(PollingStations pollingStations)
        {
            _context.PollingStations.Add(pollingStations);
            _context.SaveChanges();
            return pollingStations;
        }

        public void Update(PollingStations pollingStationParam)
        {
            var pollingStation = _context.PollingStations.Find(pollingStationParam.Id);

            if (pollingStation == null)
                throw new AppException("User not found");


            // update user properties
            pollingStation.ArabicName = pollingStationParam.ArabicName;
            pollingStation.Latitude = pollingStationParam.Latitude;
            pollingStation.Longitude = pollingStationParam.Longitude;
            pollingStation.WilayatCode = pollingStationParam.WilayatCode;
            pollingStation.Name = pollingStationParam.Name;
            pollingStation.IsActive = pollingStationParam.IsActive;
            pollingStation.IsUnifiedPollingCenter = pollingStationParam.IsUnifiedPollingCenter;
            _context.PollingStations.Update(pollingStation);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pollingStations = _context.PollingStations.Find(id);
            if (pollingStations != null)
            {
                _context.PollingStations.Remove(pollingStations);
                _context.SaveChanges();
            }
        }
    }
    public interface IKiosksService
    {
        IEnumerable<Kiosks> GetAll();
        Kiosks GetById(int id);
        IQueryable<Kiosks> GetBySerialId(string serial);
        IQueryable<Object> GetKiosksAssignedByWilayatsId(string id);
        IEnumerable<Kiosks> GetKiosksVotingStatusByWilayatsId(string id);
        IQueryable<Object> GetKiosksAssignedByGovernorateId(string id);
        IQueryable<Object> GetKiosksAssignedAll();
        IEnumerable<Kiosks> GetKiosksByPollingStationId(int id);
        IQueryable<Object> GetKiosksLockedStatusByWilayatsId(string id);
        List<Kiosks> GetKiosksByWilayatsId(string id);
        Kiosks Create(Kiosks wilayats);
        void Update(Kiosks wilayats);
        void Delete(int id);
    }

    public class KiosksService : IKiosksService
    {
        private DataContext _context;

        public KiosksService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Kiosks> GetAll()
        {
            var temp = _context.Kiosks.Include(values => values.PollingStation);
            return temp.Include(values => values.Wilayat.Governorate);
        }

        public IEnumerable<Kiosks> GetKiosksByPollingStationId(int id)
        {
            return _context.Kiosks.Include(values => values.PollingStation).Include(values => values.Wilayat).Where(values => values.PollingStationID == id);
        }

        public Kiosks GetById(int id)
        {
            return _context.Kiosks.Find(id);
        }
        public IEnumerable<Kiosks> GetKiosksVotingStatusByWilayatsId(string id)
        {
            return _context.Kiosks.Include(values => values.PollingStation).Where(values => values.WilayatCode == id);
        }

        public IQueryable<Kiosks> GetBySerialId(string serial)
        {
            return _context.Kiosks.Include(values => values.PollingStation).Where(values => values.SerialNumber == serial);
        }

        public IQueryable<Object> GetKiosksLockedStatusByWilayatsId(string id)
        {
            //return from pollingstationslist in _context.PollingStations
            //       join kioskslist in _context.Kiosks on  pollingstationslist.Id equals kioskslist.PollingStationID
            //       where pollingstationslist.WilayatCode == id
            //       group kioskslist by kioskslist.PollingDayStatus into output
            //       select new
            //       {
            //           name = output.Key,
            //           count = output.Count()
            //       };
            return from kioskslist in _context.Kiosks
                   where kioskslist.WilayatCode == id
                   group kioskslist by kioskslist.PollingDayStatus into output
                   select new
                   {
                       name = output.Key,
                       count = output.Count()
                   };
            //return _context.Kiosks.Where(values => values.WilayatCode == id).GroupBy(values => values.PollingDayStatus);
        }
        public IQueryable<Object> GetKiosksAssignedByWilayatsId(string id)
        {
            return from kioskslist in _context.Kiosks
                   join kiosksassign in _context.KiosksAssign on kioskslist.Id equals kiosksassign.KioskId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   where kioskslist.WilayatCode == id
                   select new
                   {
                       Id = kioskslist.Id,
                       SerialNumber = kioskslist.SerialNumber,
                       PollingDayStatus = kioskslist.PollingDayStatus,
                       OpenTime = kioskslist.OpenTime,
                       HasIssue = kioskslist.HasIssue,
                       CloseTime = kioskslist.CloseTime,
                       UnlockCode = kioskslist.UnlockCode,
                       WilayatCode = kioskslist.WilayatCode,
                       IsActive = kioskslist.IsActive,
                       IsUnifiedKiosk = kioskslist.IsUnifiedKiosk,
                       AreVotersPresentAsWitnesses = kioskslist.AreVotersPresentAsWitnesses,
                       IsNoFingerprintKiosk = kioskslist.IsNoFingerprintKiosk,
                       NoOfVotes = kioskslist.NoOfVotes,
                       LastRegisteredVoteAt = kioskslist.LastRegisteredVoteAt,
                       PollingStationID = kioskslist.PollingStationID,
                       PollingStation = kioskslist.PollingStation,
                       kiosksAssigned = assigns,
                       kiosksAssignedBy = assigns.member
                   };
        }

        public IQueryable<Object> GetKiosksAssignedByGovernorateId(string id)
        {
            return from kioskslist in _context.Kiosks
                   join kiosksWilayat in _context.Wilayats on kioskslist.WilayatCode equals kiosksWilayat.Code
                   join kiosksassign in _context.KiosksAssign on kioskslist.Id equals kiosksassign.KioskId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   where kiosksWilayat.GovernorateCode == id
                   select new
                   {
                       Id = kioskslist.Id,
                       SerialNumber = kioskslist.SerialNumber,
                       PollingDayStatus = kioskslist.PollingDayStatus,
                       OpenTime = kioskslist.OpenTime,
                       HasIssue = kioskslist.HasIssue,
                       CloseTime = kioskslist.CloseTime,
                       UnlockCode = kioskslist.UnlockCode,
                       WilayatCode = kioskslist.WilayatCode,
                       IsActive = kioskslist.IsActive,
                       IsUnifiedKiosk = kioskslist.IsUnifiedKiosk,
                       AreVotersPresentAsWitnesses = kioskslist.AreVotersPresentAsWitnesses,
                       IsNoFingerprintKiosk = kioskslist.IsNoFingerprintKiosk,
                       NoOfVotes = kioskslist.NoOfVotes,
                       LastRegisteredVoteAt = kioskslist.LastRegisteredVoteAt,
                       PollingStationID = kioskslist.PollingStationID,
                       PollingStation = kioskslist.PollingStation,
                       kiosksAssigned = assigns,
                       kiosksAssignedBy = assigns.member
                   };
        }

        public IQueryable<Object> GetKiosksAssignedAll()
        {
            return from kioskslist in _context.Kiosks
                   join kiosksassign in _context.KiosksAssign on kioskslist.Id equals kiosksassign.KioskId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   select new
                   {
                       Id = kioskslist.Id,
                       SerialNumber = kioskslist.SerialNumber,
                       PollingDayStatus = kioskslist.PollingDayStatus,
                       OpenTime = kioskslist.OpenTime,
                       HasIssue = kioskslist.HasIssue,
                       CloseTime = kioskslist.CloseTime,
                       UnlockCode = kioskslist.UnlockCode,
                       WilayatCode = kioskslist.WilayatCode,
                       IsActive = kioskslist.IsActive,
                       IsUnifiedKiosk = kioskslist.IsUnifiedKiosk,
                       AreVotersPresentAsWitnesses = kioskslist.AreVotersPresentAsWitnesses,
                       IsNoFingerprintKiosk = kioskslist.IsNoFingerprintKiosk,
                       NoOfVotes = kioskslist.NoOfVotes,
                       LastRegisteredVoteAt = kioskslist.LastRegisteredVoteAt,
                       PollingStationID = kioskslist.PollingStationID,
                       PollingStation = kioskslist.PollingStation,
                       kiosksAssigned = assigns,
                       kiosksAssignedBy = assigns.member
                   };
        }



        public List<Kiosks> GetKiosksByWilayatsId(string id)
        {
            return _context.Kiosks.Where(values => values.WilayatCode == id).ToList();

            //var robotOwners = (from o in context.RobotOwners
            //                   join d in context.RobotDogs
            //                   on d.DogOwnerId equals o.DogOwnerId
            //                   join f in context.RobotFactories
            //                   on f.RobotFactoryId equals d.RobotFactoryId
            //                   where f.Location == "Texas"
            //                   select o).ToList();
            //{
            //return _context.Kiosks.Where(values => values.WilayatCode == id).ToList();
        }

        public Kiosks Create(Kiosks kiosks)
        {
            _context.Kiosks.Add(kiosks);
            _context.SaveChanges();
            return kiosks;
        }

        public void Update(Kiosks kiosksParam)
        {
            var kiosks = _context.Kiosks.Find(kiosksParam.Id);

            if (kiosks == null)
                throw new AppException("User not found");


            // update user properties
            kiosks.IsActive = kiosksParam.IsActive;
            kiosks.IsNoFingerprintKiosk = kiosksParam.IsNoFingerprintKiosk;
            kiosks.NoOfVotes = kiosksParam.NoOfVotes;
            kiosks.LastRegisteredVoteAt = kiosksParam.LastRegisteredVoteAt;
            kiosks.IsUnifiedKiosk = kiosksParam.IsUnifiedKiosk;
            kiosks.WilayatCode = kiosksParam.WilayatCode;
            kiosks.OpenTime = kiosksParam.OpenTime;
            kiosks.PollingDayStatus = kiosksParam.PollingDayStatus;
            kiosks.PollingStationID = kiosksParam.PollingStationID;
            kiosks.SerialNumber = kiosksParam.SerialNumber;
            kiosks.UnlockCode = kiosksParam.UnlockCode;
            kiosks.WilayatCode = kiosksParam.WilayatCode;
            _context.Kiosks.Update(kiosks);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var kiosks = _context.Kiosks.Find(id);
            if (kiosks != null)
            {
                _context.Kiosks.Remove(kiosks);
                _context.SaveChanges();
            }
        }
    }

    public interface IKiosksAssignmentService
    {
        IEnumerable<KiosksAssign> GetAll();
        KiosksAssign GetById(int id);
        IQueryable<object> GetUsersByKiosksId(int id);
        IQueryable<object> GetUsersByWilayatsId(string id);
        IQueryable<object> GetUsersByGovernorateId(string id);
        IQueryable<object> GetUsersAll();
        IQueryable<object> GetUsersByPollingStationId(int id);
        List<KiosksAssign> GetKiosksByPollingStationId(int id);
        KiosksAssign Create(KiosksAssign wilayats);
        object Update(int id, KiosksAssign kiosksassign);
        void Delete(int id);
        object AssignKiosksToUser(KiosksAssign kiosksassign);
    }

    public class KiosksAssignmentService : IKiosksAssignmentService
    {
        private DataContext _context;

        public KiosksAssignmentService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<KiosksAssign> GetAll()
        {
            return _context.KiosksAssign.Include(values => values.PollingStation).Include(values => values.member).Include(values => values.kiosks);
        }

        public KiosksAssign GetById(int id)
        {
            return _context.KiosksAssign.Find(id);
        }



        public List<KiosksAssign> GetKiosksByPollingStationId(int id)
        {
            return _context.KiosksAssign.Where(values => values.PollingStationID == id).ToList();
        }

        public IQueryable<object> GetUsersByWilayatsId(string id)
        {
            return from users in _context.Users
                   join kiosksassign in _context.KiosksAssign on users.Id equals kiosksassign.MemberId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   where users.WilayatCode == id
                   select new
                   {
                       Id = users.Id,
                       NameEnglish = users.NameEnglish,
                       NameArabic = users.NameArabic,
                       Username = users.Username,
                       Phone = users.Phone,
                       Email = users.Email,
                       ImageUrl = users.ImageUrl,
                       CommiteeType = users.CommiteeType,
                       Gender = users.Gender,
                       PollingStation = users.PollingStation,
                       Kiosks = users.Kiosks,
                       RoleId = users.RoleId,
                       roles = users.Roles,
                       kiosksAssigned = assigns,
                       kiosksAssignedKiosk = assigns.kiosks
                   };
        }
        public IQueryable<object> GetUsersByGovernorateId(string id)
        {
            return from users in _context.Users
                   join kiosksassign in _context.KiosksAssign on users.Id equals kiosksassign.MemberId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   where users.GovernorateCode == id
                   select new
                   {
                       Id = users.Id,
                       NameEnglish = users.NameEnglish,
                       NameArabic = users.NameArabic,
                       Username = users.Username,
                       Phone = users.Phone,
                       Email = users.Email,
                       ImageUrl = users.ImageUrl,
                       CommiteeType = users.CommiteeType,
                       Gender = users.Gender,
                       PollingStation = users.PollingStation,
                       Kiosks = users.Kiosks,
                       RoleId = users.RoleId,
                       roles = users.Roles,
                       kiosksAssigned = assigns,
                       kiosksAssignedKiosk = assigns.kiosks
                   };
        }
        public IQueryable<object> GetUsersAll()
        {
            return from users in _context.Users
                   join kiosksassign in _context.KiosksAssign on users.Id equals kiosksassign.MemberId into kiosksassigns
                   from assigns in kiosksassigns.DefaultIfEmpty()
                   select new
                   {
                       Id = users.Id,
                       NameEnglish = users.NameEnglish,
                       NameArabic = users.NameArabic,
                       Username = users.Username,
                       Phone = users.Phone,
                       Email = users.Email,
                       ImageUrl = users.ImageUrl,
                       CommiteeType = users.CommiteeType,
                       Gender = users.Gender,
                       PollingStation = users.PollingStation,
                       Kiosks = users.Kiosks,
                       RoleId = users.RoleId,
                       roles = users.Roles,
                       kiosksAssigned = assigns,
                       kiosksAssignedKiosk = assigns.kiosks
                   };
        }

        public IQueryable<object> GetUsersByPollingStationId(int id)
        {
            return from users in _context.Users
                   join kiosksassigns in _context.KiosksAssign on users.Id equals kiosksassigns.MemberId
                   where users.PollingStationId == id
                   select new
                   {
                       Id = users.Id,
                       NameEnglish = users.NameEnglish,
                       NameArabic = users.NameArabic,
                       Username = users.Username,
                       Phone = users.Phone,
                       Email = users.Email,
                       ImageUrl = users.ImageUrl,
                       CommiteeType = users.CommiteeType,
                       Gender = users.Gender,
                       PollingStation = users.PollingStation,
                       Kiosks = users.Kiosks,
                       RoleId = users.RoleId,
                       roles = users.Roles,
                       KiosksAssigned = kiosksassigns
                   };
        }

        public IQueryable<object> GetUsersByKiosksId(int id)
        {
            return from users in _context.Users
                   join kiosksassigns in _context.KiosksAssign on users.Id equals kiosksassigns.MemberId
                   where kiosksassigns.KioskId == id
                   select new
                   {
                       assignedbymember = kiosksassigns.assignedbymember,
                       attendanceCompletedAt = kiosksassigns.AttendanceCompletedAt,
                       attendanceStartedAt = kiosksassigns.AttendanceStartedAt,
                       id = kiosksassigns.Id,
                       isDeleted = kiosksassigns.isDeleted,
                       kioskId = kiosksassigns.KioskId,
                       kiosks = kiosksassigns.kiosks,
                       member = kiosksassigns.member,
                       memberId = kiosksassigns.MemberId,
                       PollingStation = users.PollingStation,
                       pollingStationID = kiosksassigns.PollingStationID,
                       RoleId = users.RoleId,
                       roles = users.Roles

                   };
        }

        public KiosksAssign Create(KiosksAssign kiosks)
        {
            _context.KiosksAssign.Add(kiosks);
            _context.SaveChanges();
            return kiosks;
        }

        public object Update(int id, KiosksAssign kiosksAssignParam)
        {
            var kiosks = _context.KiosksAssign.FirstOrDefault(e => e.Id == id);

            if (kiosks == null)
                throw new AppException("Kiosks Assignment not found");


            // update user properties
            kiosks.KioskId = kiosksAssignParam.KioskId;
            kiosks.AssignedBy = kiosksAssignParam.AssignedBy;
            kiosks.isDeleted = kiosksAssignParam.isDeleted;
            kiosks.MemberId = kiosksAssignParam.MemberId;
            kiosks.PollingStationID = kiosksAssignParam.PollingStationID;
            kiosks.AttendanceStartedAt = kiosksAssignParam.AttendanceStartedAt;
            kiosks.AttendanceCompletedAt = kiosksAssignParam.AttendanceCompletedAt;

            _context.KiosksAssign.Update(kiosks);
            _context.SaveChanges();
            return kiosksAssignParam;
        }

        public void Delete(int id)
        {
            var kiosksAssign = _context.KiosksAssign.Find(id);
            if (kiosksAssign != null)
            {
                _context.KiosksAssign.Remove(kiosksAssign);
                _context.SaveChanges();
            }
        }

        object IKiosksAssignmentService.AssignKiosksToUser(KiosksAssign kiosksassign)
        {
            var kiosksAssignTemp = _context.KiosksAssign.SingleOrDefault(x => x.MemberId == kiosksassign.MemberId);
            if (kiosksAssignTemp != null)
            {
                _context.KiosksAssign.Remove(kiosksAssignTemp);
                _context.SaveChanges();
            }
            _context.KiosksAssign.Add(kiosksassign);
            _context.SaveChanges();
            return kiosksassign;
        }
    }

    public interface IWitnessService
    {
        IEnumerable<Witness> GetAll();
        Witness GetById(int id);
        IQueryable<Object> GetWitnessByWilayatsId(string id);
        Witness Create(Witness witness);
        void Update(Witness witness);
        void Delete(int id);
    }
    public class WitnessService : IWitnessService
    {
        private DataContext _context;

        public WitnessService(DataContext context)
        {
            _context = context;
        }
        public IQueryable<Object> GetWitnessByWilayatsId(string id)
        {
            return from witness in _context.Witness
                   join wilayats in _context.Wilayats on witness.WilayatCode equals wilayats.Code
                   where witness.WilayatCode == id
                   select new
                   {
                       witness.Id,
                       witness.ImageUrl,
                       witness.PollingStationID,
                       witness.UploadedTime,
                       witness.UploadedBy,
                       witness.WilayatCode,
                       witness.uploadedByMember,
                       witness.PollingStation,
                       witness.Wilayat
                   };
        }
        public IEnumerable<Witness> GetAll()
        {
            return _context.Witness;
        }

        public Witness GetById(int id)
        {
            return _context.Witness.Find(id);
        }

        public Witness Create(Witness witness)
        {
            _context.Witness.Add(witness);
            _context.SaveChanges();
            return witness;
        }

        public void Update(Witness witnessParam)
        {
            var witness = _context.Witness.Find(witnessParam.Id);

            if (witness == null)
                throw new AppException("User not found");


            // update user properties
            witness.Id = witnessParam.Id;
            witness.ImageUrl = witnessParam.ImageUrl;
            witness.PollingStationID = witnessParam.PollingStationID;
            witness.UploadedBy = witnessParam.UploadedBy;
            witness.UploadedTime = witnessParam.UploadedTime;
            witness.WilayatCode = witnessParam.WilayatCode;
            _context.Witness.Update(witness);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var witness = _context.Witness.Find(id);
            if (witness != null)
            {
                _context.Witness.Remove(witness);
                _context.SaveChanges();
            }
        }
    }

    public interface IMessagingService
    {
        IEnumerable<MessagingModel> GetAll();
        MessagingModel GetById(int id);
        IQueryable<Object> GetMessagingByWilayatsId(string id);
        MessagingModel Create(MessagingModel messaging);
        void Update(MessagingModel messaging);
        void Delete(int id);
    }
    public class MessagingService : IMessagingService
    {
        private DataContext _context;

        public MessagingService(DataContext context)
        {
            _context = context;
        }
        public IQueryable<Object> GetMessagingByWilayatsId(string id)
        {
            return _context.Messaging.Include(values => values.CreatedBy).Include(values => values.CreatedToUser).Where(values => values.WilayatCode == id || values.WilayatCode == null);
        }
        public IEnumerable<MessagingModel> GetAll()
        {
            return _context.Messaging;
        }

        public MessagingModel GetById(int id)
        {
            return _context.Messaging.Find(id);
        }

        public MessagingModel Create(MessagingModel messaging)
        {
            _context.Messaging.Add(messaging);
            _context.SaveChanges();
            return messaging;
        }

        public void Update(MessagingModel messagingParam)
        {
            var messaging = _context.Messaging.Find(messagingParam.Id);

            if (messaging == null)
                throw new AppException("Message not found");


            // update user properties
            messaging.Id = messagingParam.Id;
            messaging.WilayatCode = messagingParam.WilayatCode;
            messaging.By = messagingParam.By;
            messaging.CreatedAt = messagingParam.CreatedAt;
            messaging.Message = messagingParam.Message;
            messaging.To = messagingParam.To;
            messaging.WilayatCode = messagingParam.WilayatCode;
            _context.Messaging.Update(messaging);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var messaging = _context.Messaging.Find(id);
            if (messaging != null)
            {
                _context.Messaging.Remove(messaging);
                _context.SaveChanges();
            }
        }
    }

    public interface ICountingSoftwareService
    {
        IEnumerable<CountingSoftwareUsers> GetAll();
        CountingSoftwareUsers GetById(int id);
        List<CountingSoftwareUsers> GetWilayatsByWilayatId(string id);
        List<CountingSoftwareUsers> GetWilayatsByWilayatIdRoleId(string code, int roleid);
        List<CountingSoftwareUsers> GetWilayatsByRoleId(int roleid);
        CountingSoftwareUsers Create(CountingSoftwareUsers wilayats);
        void Update(CountingSoftwareUsers wilayats);
        void Delete(int id);
    }

    public class CountingSoftwareService : ICountingSoftwareService
    {
        private DataContext _context;

        public CountingSoftwareService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<CountingSoftwareUsers> GetAll()
        {
            return _context.CountingSoftwareUsers.Include(values => values.Wilayat);
        }

        public CountingSoftwareUsers GetById(int id)
        {
            return _context.CountingSoftwareUsers.Find(id);
        }

        public List<CountingSoftwareUsers> GetWilayatsByWilayatId(string code)
        {
            return _context.CountingSoftwareUsers.Where(values => values.WilayatCode == code).ToList();
        }

        public List<CountingSoftwareUsers> GetWilayatsByWilayatIdRoleId(string code, int roleid)
        {
            return _context.CountingSoftwareUsers.Where(values => values.WilayatCode == code && values.RoleId == roleid).ToList();
        }
        public List<CountingSoftwareUsers> GetWilayatsByRoleId(int roleid)
        {
            return _context.CountingSoftwareUsers.Where(values => values.RoleId == roleid).ToList();
        }
        

        public CountingSoftwareUsers Create(CountingSoftwareUsers user)
        {
            _context.CountingSoftwareUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(CountingSoftwareUsers user)
        {
            var countingsoftwareusers = _context.CountingSoftwareUsers.Find(user.Id);

            if (countingsoftwareusers == null)
                throw new AppException("User not found");


            // update user properties
            countingsoftwareusers.Password = user.Password;
            countingsoftwareusers.RoleId = user.RoleId;
            countingsoftwareusers.Username = user.Username;
            countingsoftwareusers.WilayatCode = user.WilayatCode;
            _context.CountingSoftwareUsers.Update(countingsoftwareusers);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var countingsoftwareusers = _context.CountingSoftwareUsers.Find(id);
            if (countingsoftwareusers != null)
            {
                _context.CountingSoftwareUsers.Remove(countingsoftwareusers);
                _context.SaveChanges();
            }
        }
    }
}
