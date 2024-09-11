using System;
using System.ComponentModel.DataAnnotations;

namespace MvcSample.Models {

    [AttributeUsage(AttributeTargets.Property,
        AllowMultiple =false,Inherited =true)]
    public class ValidBirthDay : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value is DateOnly birthday) {
                // 今日より未来の日付ならエラーとする
                if (birthday > DateOnly.FromDateTime(DateTime.Now)) {
                    return new ValidationResult("誕生日が未来の日付にはできません。");
                }

                // 正常な場合は成功とする
                return ValidationResult.Success;
            }

            // DateOnly でない場合は無効な値としてエラーを返す
            return new ValidationResult("誕生日の日付が不正です。");
        }
    }
}
