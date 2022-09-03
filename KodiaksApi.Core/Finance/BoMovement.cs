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

            if (string.IsNullOrEmpty(request.EvidenceUrl))
                throw new Exception("Evidencia vacía.");
        }
        public async Task<List<MovementSelEntity>> GetMovement(long? id = null)
        {
            var response = await DaMovement.Instance.GetMovement(id);
            return response;
        }
        public async Task<MovementEntity> NewMovement(MovementEntity request)
        {
            var response = await DaMovement.Instance.NewMovement(request);
            return response;
        }
        public async Task<MovementEntity> EditMovement(MovementEntity request)
        {
            var response = await DaMovement.Instance.EditMovement(request);
            return response;
        }
        public async Task<MovementEntity> DeleteMovement(long? incomeId)
        {
            var response = await DaMovement.Instance.DeleteMovement(incomeId);
            return response;
        }
    }
}
