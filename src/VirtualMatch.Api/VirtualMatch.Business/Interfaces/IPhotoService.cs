﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoDto> AddPhotoAsync(IFormFile file, string username);
        Task<bool> DeletePhotoAsync(string username, int photoId);

        Task<bool> SetMainPhoto(string username, int photoId);
    }
}
