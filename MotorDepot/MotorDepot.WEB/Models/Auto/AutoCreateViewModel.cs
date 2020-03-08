using System.ComponentModel.DataAnnotations;

namespace MotorDepot.WEB.Models.Auto
{
    public class AutoCreateViewModel
    {
        [Required, StringLength(60, MinimumLength = 3)]
        public string Model { get; set; }
        [Required, StringLength(10, MinimumLength = 2)]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{4}$", ErrorMessage = "Auto numbers must be AX12AX1234")]
        public string Numbers { get; set; }
        [Required, Range(1, 500)]
        public int EnginePower { get; set; }
        [Required, Range(20, 500)]
        public double EngineCapacity { get; set; }
        [Required, Range(5, 500)]
        public double BootVolumeMax { get; set; }
        public int AutoBrandId { get; set; }
        public int AutoTypeId { get; set; }
    }
}