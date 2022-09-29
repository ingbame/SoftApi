using KodiaksApi.Data;
using KodiaksApi.Data.Finance;
using KodiaksApi.Entity.Finance;

namespace KodiaksApi.Core.Finance
{
    public class BoMovement
    {
        #region Patron de Diseño
        private static BoMovement _instance;
        private static readonly object _instanceLock = new object();
        public static BoMovement Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoMovement();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public void isValidMovementModel(MovementEntity request)
        {
            if (!request.MemberId.HasValue || request.MemberId <= 0)
                throw new Exception("La persona que está capturando el movimiento no es válida, asegurese de tener permisos.");

            if (!request.MovementTypeId.HasValue || request.MovementTypeId <= 0)
                throw new Exception("No ha seleccionado un tipo de movimiento válido.");

            if (!request.ConceptId.HasValue || request.ConceptId <= 0)
                throw new Exception("No ha seleccionado un cocepto válido.");

            if (!request.MethodId.HasValue || request.MethodId <= 0)
                throw new Exception("No ha seleccionado un método de pago válido.");

            if (!request.MovementDate.HasValue)
                throw new Exception("La fecha es incorrecta.");
            if (request.MovementDate.Value.Date > DateTime.Now.Date)
                throw new Exception("La fecha es mayor al día de hoy.");

            if (!request.Amount.HasValue || request.Amount <= 0)
                throw new Exception("El monto es incorrecto, debe ser un número mayor a $0.00.");
            if(!request.CreatedBy.HasValue || request.CreatedBy <= 0)
                throw new Exception("No se puede guardar, si su sesion no está activa.");
        }
        public async Task<List<MovementSelEntity>> GetMovement(long? id = null)
        {
            var response = await DaMovement.Instance.GetMovement(id);
            return response;
        }
        public async Task<decimal> GetTotal()
        {
            var response = await DaMovement.Instance.GetTotal();

            return response;
        }
        public async Task<MovementEntity> NewMovement(MovementEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!string.IsNullOrEmpty(request.ByUser))
            {
                var credential = DaSecurity.Instance.GetUser(request.ByUser);
                if (credential != null)
                    request.CreatedBy = credential.User.UserId;
                else
                    throw new Exception("Usuario no existe.");
            }

            isValidMovementModel(request);
            var response = await DaMovement.Instance.NewMovement(request);
            return response;
        }
        public async Task<MovementEntity> EditMovement(long? id, MovementEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.MovementId)
                throw new Exception("Debe de coincidir el id del movimiento con la petición.");
            isValidMovementModel(request);
            var response = await DaMovement.Instance.EditMovement(request);
            return response;
        }
        public async Task<MovementEntity> DeleteMovement(MovementEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.MovementId.HasValue || request.MovementId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaMovement.Instance.DeleteMovement(request.MovementId);
            return response;
        }
    }
}
