using MudBlazor;

namespace OOAP1.Models
{
    public interface IElectronicOpt
    {
         ElectronicBase? OrderGood();
    }

    public class ElectronicOpt:IElectronicOpt
    {
         public  ElectronicBase? OrderGood()
        {
            ElectronicBase result = null;

            switch(new Random().Next(1, 4)) 
            {
                case 1: result = new CPU();
                        break;
                case 2: result = new HDD();
                    break;
                case 3: result = new Motherboard();
                    break;
            
            }
            return result;
        }
    }
    public class ELectronicOptProxy : IElectronicOpt
    {
        private readonly ElectronicOpt electronicOpt;

        public ISnackbar _snackbar;

        private readonly string _role;
        public ELectronicOptProxy(ElectronicOpt electronicOpt, ISnackbar snackbar, string role)
        {
            this.electronicOpt = electronicOpt;
            this._snackbar = snackbar;          
            _role = role;
        }
        public ElectronicBase? OrderGood()
        {
            if(_role.ToLower() == "кладовщик")
            {
                _snackbar.Add("Заках создан");
                return electronicOpt.OrderGood();
            }
            else
            {
                _snackbar.Add("У вашей роли недостаточно прав");
                return null;
            }
        }
    }
}
