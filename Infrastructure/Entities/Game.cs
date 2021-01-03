using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Entities
{
	public class Game
	{
		/// <summary>
		/// ID
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int GameId { get; set; }
		/// <summary>
		/// Название
		/// </summary>
		[Display(Name = "Название")]
		[Required(ErrorMessage = "Пожалуйста, введите название игры")]
		public string Name { get; set; }
		/// <summary>
		/// описание
		/// </summary>
		[DataType(DataType.MultilineText)]
		[Display(Name = "Описание")]
		[Required(ErrorMessage = "Пожалуйста, введите описание для игры")]		
		public string Description { get; set; }
		/// <summary>
		/// Категория
		/// </summary>
		[Display(Name = "Категория")]
		[Required(ErrorMessage = "Пожалуйста, укажите категорию для игры")]
		public string Category { get; set; }
		/// <summary>
		/// Цена
		/// </summary>
		[Display(Name = "Цена (руб)")]
		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
		public decimal Price { get; set; }
	}
}
