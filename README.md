# CalculadoraDeCotacoes

### Ferramentas necessárias
    O Docker Desktop é necessário para que os containers possam ser criados e iniciados localmente.
	https://docs.docker.com/desktop/setup/install/windows-install/


Os passos abaixo estão por ordem de execução

# Executar os comandos abaixo no terminal ou powershell
dotnet dev-certs https --clean
dotnet dev-certs https --trust
dotnet dev-certs https -ep $env:USERPROFILE/.aspnet/https/aspnetapp.pfx -p Senha@123


  ### Visual Studio
     Clicar com o botão direito no arquivo docker-compose project > Set as StartUp project. 
     Depois basta clicar em Iniciar.
 
  ### Rider 
     Clicar com o botão esquerdo no projeto docker-compose para expandir.
     Então basta clicar com o botão direito no arquivo "docker-compose.yml" e selecionar "Run 'docker-compose.yml'" 

Após seguir os passos acima, o contêiner deve estar em execução com todas as imagens devidamente configuradas.
Toda a comunicação entre a WebApi e o banco de dados já está configurada para facilitar o processo de teste.
Agora o projeto está pronto para ser testado.

## Web Api 
- Endereço: https://localhost:5001/swagger/
- Description: Este link fornece acesso à documentação do Swagger, onde é possível testar a API e ver como cada método funciona.
