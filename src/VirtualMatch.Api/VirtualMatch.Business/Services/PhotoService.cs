using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;
using VirtualMatch.Entities.Helpers;

namespace VirtualMatch.Business.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PhotoService(IOptions<CloudinarySettings> config, IUserRepository userRepository, IMapper mapper)
        {
            var acc = new Account
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            };

            _cloudinary = new Cloudinary(acc);

            _userRepository = userRepository;

            _mapper = mapper;
        }
        public async Task<PhotoDto> AddPhotoAsync(IFormFile file, string username)
        {
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            Photo photo = new Photo
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };

            await this._userRepository.AddPhotoAsync(photo, username);

            if (!await _userRepository.SaveAllAsync())
                throw new Exception("Smething went wrong while adding photo");
            

            return _mapper.Map<PhotoDto>(photo);

            
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task<bool> SetMainPhoto(string username, int photoId)
        {
            return await this._userRepository.SetMainPhoto(username, photoId);
        }
    }
}
