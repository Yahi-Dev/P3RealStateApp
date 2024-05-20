using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.ViewModels.Domain.ClientFavoriteProperty;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;

namespace RealStateApp.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IImprovementService _improvementService;
        private readonly IPropertyImprovementService _propertyImprovementService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISaleCategoryService _saleCategoryService;
        private readonly IClientFavoritePropertyService _clientFavoritePropertyService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse _userViewModel;
        public PropertyController(
            IPropertyService propertyService, 
            IPropertyTypeService propertyTypeService, 
            IImprovementService improvementService, 
            IPropertyImprovementService propertyImprovementService, 
            IPropertyImageService propertyImageService, 
            IHttpContextAccessor httpContextAccessor, 
            ISaleCategoryService saleCategoryService, 
            IClientFavoritePropertyService clientFavoritePropertyService,
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _improvementService = improvementService;
            _propertyImprovementService = propertyImprovementService;
            _propertyImageService = propertyImageService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _saleCategoryService = saleCategoryService;
            _clientFavoritePropertyService = clientFavoritePropertyService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _propertyService.GetInfoByIdViewModel(id);
            return View(result);

        }
        public async Task<IActionResult> QuitFavoriteProperty(int propertyid)
        {
            
            var currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var favProperties = await _clientFavoritePropertyService.GetAllViewModel();
            var selected = favProperties.Find(e => e.ClientId == currentUser.Id && e.PropertyId == propertyid);
            await _clientFavoritePropertyService.Delete(selected.Id);

            return View("Home", "Index");
        }

        public async Task<IActionResult> SetFavoriteProperty(int propertyid)
        {

            var currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            var saveClientFavoriteProperty = new SaveClientFavoritePropertyViewModel()
            {
                ClientId = currentUser.Id,
                PropertyId = propertyid
            };
            await _clientFavoritePropertyService.Add(saveClientFavoriteProperty);

            return View("Home", "Index");
        }

        public async Task<IActionResult> CreateProperty()
        {
            SavePropertyViewModel vm = new();
            vm.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            vm.SalesCategories = await _saleCategoryService.GetAllViewModel();
            vm.Improvements = await _improvementService.GetAllViewModel();

            return View("SaveProperty", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(SavePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                    
                    ModelState.Remove("AgentId");
                    ModelState.Remove("ImagePath");
                    ModelState.Remove("IdentificationCard");
                

                vm.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                vm.SalesCategories = await _saleCategoryService.GetAllViewModel();
                vm.Improvements = await _improvementService.GetAllViewModel();

                return View("SaveProperty", vm);
            }

            SavePropertyViewModel property = await _propertyService.Add(vm);

            foreach (var improvementId in vm.Improvement)
            {
                await _propertyImprovementService.Add(new SavePropertyImprovementViewModel { PropertyId = (int)property.Id, ImprovementId = improvementId});
            }

            if (property.Id != null)
            {
                if (property.Id != null && vm.Image1 != null)
                {
                    SavePropertyImageViewModel savePropertyImage = new();

                    savePropertyImage.ImageURL = UploadFile(vm.Image1, property.Id);
                    savePropertyImage.PropertyId = property.Id;
                    savePropertyImage.UserId = _userViewModel.Id;
                    savePropertyImage.IsMain = true;
                    await _propertyImageService.Add(savePropertyImage);
                }

                if (vm.Image2 != null)
                {
                    SavePropertyImageViewModel savePropertyImage = new();

                    savePropertyImage.ImageURL = UploadFile(vm.Image2, property.Id);
                    savePropertyImage.PropertyId = property.Id;
                    savePropertyImage.UserId = _userViewModel.Id;
                    await _propertyImageService.Add(savePropertyImage);
                }

                if (vm.Image3 != null)
                {
                    SavePropertyImageViewModel savePropertyImage = new();

                    savePropertyImage.ImageURL = UploadFile(vm.Image3, property.Id);
                    savePropertyImage.PropertyId = property.Id;
                    savePropertyImage.UserId = _userViewModel.Id;
                    await _propertyImageService.Add(savePropertyImage);
                }

                if (vm.Image4 != null)
                {
                    SavePropertyImageViewModel savePropertyImage = new();

                    savePropertyImage.ImageURL = UploadFile(vm.Image4, property.Id);
                    savePropertyImage.PropertyId = property.Id;
                    savePropertyImage.UserId = _userViewModel.Id;
                    await _propertyImageService.Add(savePropertyImage);
                }

                await _propertyService.Update(property, property.Id);
            }
            return RedirectToRoute(new { controller = "Agent", action = "Index"});

        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/propiedad/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }


        public async Task<IActionResult> EditProperty(int id)
        {
            SavePropertyViewModel vm = await _propertyService.GetByIdSaveViewModel(id);
            List<int> previousImprovementIds = vm.Improvements.Select(e => e.Id).ToList();
            vm.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            vm.SalesCategories = await _saleCategoryService.GetAllViewModel();
            vm.Improvements = await _improvementService.GetAllViewModel();

            ViewBag.PreviousImprovementsIds = previousImprovementIds;

            return View("SaveProperty", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(SavePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm = await _propertyService.GetByIdSaveViewModel(vm.Id);
                List<int> previousImprovementIds = vm.Improvement;
                vm.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                vm.SalesCategories = await _saleCategoryService.GetAllViewModel();
                vm.Improvements = await _improvementService.GetAllViewModel();
                ViewBag.PreviousImprovementsIds = previousImprovementIds;
                return View("SaveProperty", vm);
            }

            SavePropertyViewModel property = await _propertyService.GetByIdSaveViewModel(vm.Id);
            vm.Code = property.Code;

            if (vm.Improvement != null && vm.Improvement.Any())
            {
                await _propertyService.RemoveAllPropertyImprovements(property.Id);

                foreach (var improvementId in vm.Improvement)
                {
                    await _propertyImprovementService.Add(new SavePropertyImprovementViewModel { PropertyId = property.Id, ImprovementId = improvementId });
                }
            }

            SavePropertyViewModel result = await _propertyService.GetByIdSaveViewModel(vm.Id);

            List<IFormFile> newFiles = new List<IFormFile> { vm.Image1, vm.Image2, vm.Image3, vm.Image4 }.Where(f => f != null).ToList();

            List<BasePropertyImageViewModel> existingImages = await _propertyService.GetPropertyImagesById(vm.Id);

            int totalImagesAllowed = 4;

            int existingImagesToRemove = Math.Max(0, existingImages.Count + newFiles.Count - totalImagesAllowed);

            for (int i = 0; i < existingImagesToRemove; i++)
            {
                await _propertyImageService.Delete(existingImages[i].Id);
            }

            bool isFirstImage = true; // Inicializa isFirstImage como true fuera del bucle foreach

            foreach (var file in newFiles)
            {
                if (vm.Image1 != null)
                {
                    string imageUrl = UploadFile(file, vm.Id);
                    SavePropertyImageViewModel saveImage = new()
                    {
                        ImageURL = imageUrl,
                        PropertyId = result.Id, // Usa result.Id en lugar de property.Id
                        IsMain = isFirstImage,
                        UserId = HttpContext.Session.Get<AuthenticationResponse>("user").Id
                    };

                    await _propertyImageService.Add(saveImage);
                    existingImages.Add(new BasePropertyImageViewModel { ImageURL = imageUrl });

                    isFirstImage = false;
                }
                else
                {
                    string imageUrl = UploadFile(file, vm.Id);
                    SavePropertyImageViewModel saveImage = new()
                    {
                        ImageURL = imageUrl,
                        PropertyId = result.Id, // Usa result.Id en lugar de property.Id
                        IsMain = false,
                        UserId = HttpContext.Session.Get<AuthenticationResponse>("user").Id
                    };

                    await _propertyImageService.Add(saveImage);
                    existingImages.Add(new BasePropertyImageViewModel { ImageURL = imageUrl });
                }
            }

            await _propertyService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePropertyPost(int id)
        {
            var vm = await _propertyService.GetByIdSaveViewModel(id);
            await _propertyService.Delete(id);

            string basePath = $"/Images/SavedPhotos/{vm.Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Property", action = "MyProperties", id = vm.AgentId });
        }
    }
}
