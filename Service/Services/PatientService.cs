﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepositories;
using Service.DTO;
using Service.Helpers;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PatientService : UserService, IPatientService
    {

        private IPatientRepository PatientRepository { get; }

        public PatientService(IUserRepository _UserRepository, IPatientRepository _PatientRepository, IConfiguration _Configuration)
            : base(_UserRepository, _Configuration)
        {
            PatientRepository = _PatientRepository;
        }

        public async Task<Patient> AddPatient(PatientDto dto)
        {
            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            Patient doctor = UserMapper.ToPatient(dto);
            doctor.PasswordHash = passwordHash;
            doctor.PasswordSalt = passwordSalt;
            return await PatientRepository.Add(doctor);
        }

        public async Task<Patient> DeletePatient(int Patient_id)
        {
            return await PatientRepository.Delete(Patient_id);
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            return await PatientRepository.GetAll().Select(u => new PatientDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Mail = u.Mail,
                NationalId = u.NationalId,
                Image = u.Image,
                Gender = u.Gender,
                BloodType = u.BloodType,
                PhoneNumber = u.PhoneNumber,
                DepartmentName = u.Department.Department_Name,
                CreatedDtm = u.CreatedDtm,
                IsActive = u.IsActive
            }).ToListAsync();
        }

        public async Task<Patient> UpdatePatient(PatientDto dto)
        {
            Patient currentPatient = await PatientRepository.GetById(dto.Id);
            currentPatient = UserMapper.UpdatePatient(dto, currentPatient);
            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            currentPatient.PasswordHash = passwordHash;
            currentPatient.PasswordSalt = passwordSalt;
            return await PatientRepository.Update(currentPatient);
        }

        public async Task<PatientDto> GetPatientById(int Patient_id)
        {
            Patient patient = await PatientRepository.GetById(Patient_id);
            PatientDto patient_dto = UserMapper.ToPatientDto(patient);
            return patient_dto;
        }

        public async Task<PatientDto> GetPatientByName(string Patientname)
        {
            Patient patient = await PatientRepository.FindByName(Patientname);
            PatientDto patient_dto = UserMapper.ToPatientDto(patient);
            return patient_dto;
        }
    }
}
