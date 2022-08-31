﻿using KodiaksApi.Data;
using KodiaksApi.Entity.Common;
using KodiaksApi.Entity.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core
{
    public class BoFinance
    {
        #region Patron de Diseño
        private static BoFinance _instance;
        private static readonly object _instanceLock = new object();
        public static BoFinance Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoFinance();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public ResponseEntity<string> ValidateIncomeModel(IncomeEntity request)
        {
            var response = new ResponseEntity<string>();
            try
            {
                if (!request.MemberId.HasValue || request.MemberId <= 0)
                    throw new Exception("La persona que está capturando el movimiento no es válida, asegurese de tener permisos.");

                if (!request.ConceptId.HasValue || request.ConceptId <= 0)
                    throw new Exception("No ha seleccionado un cocepto válido.");

                if (!request.MethodId.HasValue || request.MethodId <= 0)
                    throw new Exception("No ha seleccionado un método de pago válido.");

                if (!request.IncomeDate.HasValue)
                    throw new Exception("La fecha es incorrecta.");
                if (request.IncomeDate.Value.Date > DateTime.Now.Date)
                    throw new Exception("La fecha es mayor al día de hoy.");

                if (!request.Amount.HasValue || request.Amount <= 0)
                    throw new Exception("El monto es incorrecto, debe ser un número mayor a $0.00.");

                if (string.IsNullOrEmpty(request.EvidenceUrl))
                    throw new Exception("Evidencia vacía.");
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        public ResponseEntity<List<IncomeSelEntity>> GetIncomes()
        {
            var response = new ResponseEntity<List<IncomeSelEntity>>();
            try
            {
                response.Model = DaFinance.Instance.GetIncomes();
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        public ResponseEntity<IncomeEntity> NewIncome(IncomeEntity request)
        {
            var response = new ResponseEntity<IncomeEntity>();
            try
            {
                response.Model = DaFinance.Instance.NewIncome(request);
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}