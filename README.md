# Originários
## Projeto Final - Recode Pro 2021
### Squad 41

Desenvolvimento de um site para venda de produtos indígenas.

*Em produção...*

###### Como contribuir
- Para iniciar contribuindo: crie um fork deste repositório, clone seu fork, realize as edições na sua máquina, dê um push pro seu fork, faça um pull request para este repositório

Após o repositório local estar atualizado:

1. Rode o arquivo BancoDeDados/criarBD.sql no SQL Server
2. Abra o arquivo Originarios.sln com o Visual Studio
3. Para que os pacotes sejam ajustados, vá em Ferramentas -> Genreciador de Pacotes do NuGet -> Console do Gerenciador de Pacotes, depois execute o comando abaixo
```
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```
4. Execute a aplicação
