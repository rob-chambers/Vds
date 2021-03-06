﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vds.Data;
using Microsoft.EntityFrameworkCore;

namespace Vds.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll();
        Task<List<Person>> GetAllInvalid();
        Task<Person> GetById(Guid id);
        Task<Person> Add(Person person);
        Task<Person> Update(Person person);
        Task<Person> Delete(Guid id);
        Task SubmitList();
    }

    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAll()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<List<Person>> GetAllInvalid()
        {
            return await _context.People.Where(p => p.Status == PersonStatus.Invalid).ToListAsync();
        }

        public async Task<Person> GetById(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            return person;
        }

        public async Task<Person> Add(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> Delete(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task SubmitList()
        {
            // Simulate a status update
            var r = new Random();
            foreach (var person in await GetAll())
            {
                var number = r.Next(4);
                person.Status = (PersonStatus) number;
                await Update(person);
            }
        }
    }
}
