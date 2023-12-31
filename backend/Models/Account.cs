﻿using backend.Validations;

namespace backend.Models;

public class Account
{
    public int Id { get; set; }

    [CpfCnpjValidation(ErrorMessage = "CPF/CNPJ inválido")]
    public string CpfCnpj { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool AccountStatus { get; set; } = true;

    public Role Role { get; set; }
    public virtual ICollection<Transactions> Transactions { get; } = new List<Transactions>();
}
