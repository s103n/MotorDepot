﻿using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private readonly ApplicationContext _context;

        public FlightRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Flight item)
        {
            _context.Flights.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flight item)
        {
            _context.Flights.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight item)
        {
            _context.Set<Flight>().AddOrUpdate(item);

            await _context.SaveChangesAsync();
        }

        public async Task<Flight> FindAsync(int? id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
