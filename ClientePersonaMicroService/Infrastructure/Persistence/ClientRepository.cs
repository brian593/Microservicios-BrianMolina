using System;
using ClientePersonaMicroService.Application.Interfaces;
using ClientePersonaMicroService.Domain.Entities;
using ClientePersonaMicroService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace ClientePersonaMicroService.Infrastructure.Persistence
{
	public class ClientRepository:IClienteRepository
	{
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
		{
            _context = context;

        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}

