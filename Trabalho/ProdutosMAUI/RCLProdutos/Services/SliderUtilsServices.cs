using RCLProdutos.Services.Interfaces;

namespace RCLProdutos.Services
{
    public class SliderUtilsServices : ISliderUtilsServices
    {
        public int _value { get; set; } = 0;
        public int Index {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                NotificationOnChange();
            }
        
        }

        private int _valueCont { get; set; } = 0;
        public int CountSlide
        {
           get
           {
                return _valueCont;
           }
           set
           {
                _valueCont = value;
                NotificationOnChange();
           }

        }

        private float _valueWidthSlide2 { get; set; } = 0.00f;

        public float WidthSlide2 
        {
            get
            {
                return _valueWidthSlide2;
            }
            set
            {
                _valueWidthSlide2 = value;
                NotificationOnChange();
            }
        }

        public List<string> _marginLeftSlide = new List<string>();

        public List<string> MarginLeftSlide {
            get 
            {
                return _marginLeftSlide;
            }
            set
            {
                _marginLeftSlide = value;
                NotificationOnChange();
            } 
        }

        // --- NOVO: quantos slides por página (desktop) ---
        private int _itensPorPagina = 2;
        public int ItensPorPagina
        {
            get => _itensPorPagina;
            set
            {
                _itensPorPagina = value <= 0 ? 1 : value;
                NotificationOnChange();
            }
        }

        // --- NOVO: estilo aplicado ao TRACK (usa Index como "pagina") ---
        // WidthSlide2 já existe e vai ser 50 quando ItensPorPagina=2
        public string TrackStyle => $"transform: translateX(-{Index * WidthSlide2}%);";

        // --- NOVO: calcula máximo de páginas com base no total de produtos ---
        public int MaxPagina(int totalProdutos)
        {
            if (totalProdutos <= 0) return 0;
            return (int)Math.Ceiling(totalProdutos / (double)ItensPorPagina) - 1;
        }


        public event Action OnChange;
        private void NotificationOnChange() => OnChange?.Invoke();
    }
}
