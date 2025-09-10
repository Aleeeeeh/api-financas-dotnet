using AFSilva.Core.Auth;
using AFSilva.Core.Auth.Password;
using AFSilva.Core.Repositories.Models;
using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Dtos.Output;
using ApiFinancas.Domain.Entities;
using ApiFinancas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiFinancas.Services;

public class UsuarioService(RepositoryBase<Usuario> repository, JwtTokenGenerator generatorToken) : IUsuarioService
{
    private readonly RepositoryBase<Usuario> _repository = repository;
    private readonly JwtTokenGenerator _generatorToken = generatorToken;

    public async Task AtualizarUsuarioAsync(int id, PutUsuarioDto dto)
    {
        Usuario usuario = await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Usuário não encontrado");

        await _repository.UpdateAsync(dto.Update(usuario));
    }

    public async Task<TokenUsuarioDto> Autenticar(string email, string senha)
    {
        Usuario usuario = await ObterUsuarioPorEmail(email);

        BcryptPasswordHasher.ComparePassword(senha, usuario.Senha);

        TokenRequest token = usuario.ObterInformacoesToken();

        string tokenJwt = _generatorToken.GenerateToken(token);

        await AtualizarUltimoAcessoUsuarioAsync(usuario);

        return new TokenUsuarioDto
        {
            UsuarioId = usuario.Id,
            NomeAcesso = usuario.NomeAcesso,
            Token = tokenJwt
        };
    }

    private async Task AtualizarUltimoAcessoUsuarioAsync(Usuario usuario)
    {
        usuario.UltimoAcesso = DateTime.UtcNow;
        await _repository.UpdateAsync(usuario);
    }

    public async Task<UsuarioDto> CriarUsuarioAsync(PostUsuarioDto dto)
    {
        ValidarSeEmailJaExiste(dto.Email);

        Usuario usuario = dto;

        await _repository.CreateAsync(usuario);

        return usuario;
    }

    private async Task<Usuario> ObterUsuarioPorEmail(string email)
    {
        return await _repository.Query()
            .FirstOrDefaultAsync(u => u.Email == email.ToLower()) ??
            throw new ArgumentException("Email informado não existe.");
    }

    private async void ValidarSeEmailJaExiste(string email)
    {
        bool emailJaExiste = await _repository.Query()
            .AnyAsync(u => u.Email == email);

        if (emailJaExiste)
            throw new ArgumentException("Email já cadastrado.");
    }

    public async Task DeletarUsuarioAsync(int id)
    {
        await _repository.RemoveAsync(id);
    }
}
