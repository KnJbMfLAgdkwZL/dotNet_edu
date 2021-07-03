using System;

namespace WebApplication.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("Попытка создать заказ для забанненого клиента")
        {
        }
    }
}