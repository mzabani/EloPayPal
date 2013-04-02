using System;
using System.Collections.Generic;
using System.Linq;

namespace Elopayments
{
	public class Validator
	{
		/// <summary>
		/// Validates a CPF document.
		/// </summary>
		/// <param name="identidade">The CPF, this can be null, empty or any kind of string.</param>
		public static bool IsCPF(string identidade) {
			if (String.IsNullOrWhiteSpace(identidade))
				return false;
			
			// Remove special chars
			identidade = identidade.Replace("-", "").Replace(".", "").Trim();
			
			int[] digitos_verificadores;
			int[] numero;
			
			try
			{
				digitos_verificadores = identidade.Substring(identidade.Length - 2).Select((char d) => Int32.Parse(d.ToString())).ToArray();
				numero = identidade.Substring(0, identidade.Length - 2).Select((char d) => Int32.Parse(d.ToString())).ToArray();
			}
			catch (FormatException)
			{
				return false;
			}
			catch (ArgumentOutOfRangeException)
			{
				return false;
			}
			
			// Verify that it is a valid CPF
			
			// 1. First verification digit
			int first_verification_digit;
			int multiplier = 10;
			int sum_digits = 0;
			foreach (int digit in numero)
				sum_digits += digit * multiplier--;
			
			int mod11 = sum_digits % 11;
			if (mod11 < 2)
				first_verification_digit = 0;
			else
				first_verification_digit = 11 - mod11;
			
			if (first_verification_digit != digitos_verificadores[0])
				return false;
			
			// 2. Second verification digit
			int second_verification_digit;
			sum_digits = 0;
			multiplier = 11;
			foreach (int digit in numero)
				sum_digits += digit * multiplier--;
			
			sum_digits += first_verification_digit * multiplier;
			
			mod11 = sum_digits % 11;
			if (mod11 < 2)
				second_verification_digit = 0;
			else
				second_verification_digit = 11 - mod11;
			
			if (second_verification_digit != digitos_verificadores[1])
				return false;
			
			return true;
		}
		
		public static string FormatarCPF(string cpf) {
			cpf = cpf.Replace("-", "").Replace(".", "").Trim();
			return cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);
		}
		
		public static bool IsCNPJ(string identidade) {
			throw new NotImplementedException();
		}
		
		public static string FormatarCNPJ(string cnpj) {
			throw new NotImplementedException();
		}
		
		public static bool IsCEP(string str) {
			if (str == null)
				return false;
			
			return System.Text.RegularExpressions.Regex.IsMatch(str, @"^\s*\d{2}\.?\d{3}-?\d{3}\s*$");
		}
		
		public static string FormatarCEP(string cep) {
			cep = cep.Replace(".", "").Replace("-", "").Trim();
			
			return cep.Substring(0, 5) + "-" + cep.Substring(5);
		}
		
		public static bool IsTelefone(string str) {
			if (String.IsNullOrWhiteSpace(str))
				return false;
			
			str = str.Replace(".", "").Replace("-", "").Replace(" ", "");
			
			return System.Text.RegularExpressions.Regex.IsMatch(str, @"^\(?[1-9][0-9]\)?[0-9]{8,}$");
		}
		
		public static string FormatarTelefone(string tel) {
			tel = tel.Replace(".", "").Replace("-", "").Replace(" ", "");
			
			return System.Text.RegularExpressions.Regex.Replace(tel, @"^\(?([1-9][0-9])\)?([0-9]{8,})$", @"($1) $2");
		}
	}
}

