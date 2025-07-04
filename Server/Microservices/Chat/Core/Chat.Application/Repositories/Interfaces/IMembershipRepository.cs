﻿using Chat.Application.DTOs.Membership;
using Chat.Application.Repositories.Abstract;
using Chat.Domain.Entity;

namespace Chat.Application.Repositories.Interfaces
{
    public interface IMembershipRepository : IBaseRepository<Membership, MembershipCreateDTO, MembershipGetDTO>
    {
    }
}
