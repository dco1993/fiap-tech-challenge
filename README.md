# FIAP Cloud Games (FCG) - Tech Challenge - Fase 1

## 🌟 Apresentação

O **FIAP Cloud Games (FCG)** é uma plataforma fictícia de venda de jogos digitais e gerenciamento de biblioteca de usuários, desenvolvida como parte do projeto do primeiro módulo da pós-graduação em Arquitetura de Sistemas .NET com Azure - FIAP.

O objetivo da fase é criar um **MVP funcional** com uma **API REST em .NET 8** para gerenciamento de usuários, jogos adquiridos e regras de acesso.

---

## ⚙️ Instruções de Execução

### 1\. Clonar o repositório

```css
git clone https://github.com/dco1993/fiap-tech-challenge.git
```

### 2\. Executar migrations e atualizar o banco

Caso escolha usar o banco de dados já configurado no arquivo App/appsettings.json, será necessário apagar as tabelas para testar as migrations.  
O comando abaixo deve ser executado na pasta raiz, onde está localizado o arquivo da solução, normalmente em:  
**fiap-tech-challenge\\fiap-cloud-games\\fiap-cloud-games.sln**

```css
dotnet-ef database update --project Infra.Data --startup-project App
```

### 3\. Executar o projeto

Da mesma forma que na etapa anterior, o comando abaixo deve ser executada na pasta raiz da solução.

```css
dotnet run --project App/App.csproj
```

### 4\. Acessar o Swagger

Se o comando da etapa anterior for executado com sucesso, um conjunto de logs parecidos com esses deve ser exibido.

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/bf3711a0fa98769b98f1c1786c70fc316b2de3901acc3a25.png)

Para executar o projeto basta **copiar o link** mostrado na linha que se inicia com "Now listening on: ..." e adicionar "/swagger" no fim e executar no navegador.

Deve ficar similar ao link abaixo.

Obs.: 0000 foi utilizado como placeholder e deve ser substituido pela porta gerada na sua máquina.

```css
http://localhost:0000/swagger
```

---

## 💡 Instruções de Uso

Ao executar a primeira Migration, o usuário chamado "Admin" é criado, por padrão com a senha "Mudar@123".

Esse usuário tem por padrão permissões de Administrador e essa permissão não pode ser retirada.

O sistema fornece duas roles (políticas de acesso):

**Usuário (User) -** Precisa ser cadastrado, pode acessar sua própria biblioteca e comprar jogos.

**Administrador (Administrator) -** Além das permissões normais de Usuário, pode cadastrar jogos, criar/desativar promoções para jogos, listar todos os usuários do sistema e alterar seus níveis de acesso (não é possível alterar o próprio usuário e o Admin).

No momento, usuários não atenticados podem acessar apenas o endpoint de listagem de jogos, cadastro e o endpoint de login, para efetuar a autenticação.

Ao acessar o Swagger é possível ver que existem 3 controllers.

1.  **Auth -** Utilizado para cadastro de usuários e login/obter token. É um controller pensado para endpoints que não precisam de autenticação.
2.  **Games -** Utilizado por usuários registrados ou visitantes. Contém os endpoints de listagem de jogos, acesso a biblioteca do usuário e compra.
3.  **Administration -** Contem os endpoints de administração do sistema. Gerenciamento de usuários, jogos, descontos e políticas de acesso.

---

#### AdministrationController

###### /api/Administration/getUsers

Verbo: **GET**  
Política: **Administrator**  
Esse endpoint permite obter uma lista dos usuáros cadastrados e seu e-mail para contato.

###### /api/Administration/getDiscounts

Verbo: **GET**  
Política: **Administrator**  
Esse endpoint retorna uma lista de todos os descontos cadastrados.

###### /api/Administration/createGame

Verbo: **POST**  
Política: **Administrator**  
Endpoint para cadastro de jogos. Deve estar estruturado da seguinte forma:

```css
{
    "title": "string",
    "genre": "string",
    "metacritic": 0,
    "publisher": "string",
    "developer": "string",
    "releaseDate": "2025-06-01",
    "about": "string",
    "price": 0
}
```

Abaixo as regras dos campos para cadastro de novo jogo:

*   string - title\* - entre 5 e 100 caracteres
*   string - genre\* - entre 3 e 50 caracteres
*   decimal - metacritic\* - a pontuação deve estar entre 0.0 e 10.0
*   string - publisher\* - entre 4 e 50 caracteres
*   string - developer\* - entre 4 e 50 caracteres
*   datetime - releaseDate\* - deve ser superior ou igual a 01/01/1753
*   string - about - sem limite de caracteres
*   decimal - price\* - deve ser maior que 0

As propriedades com \* são obrigatórias.

###### /api/Administration/createDiscount

Verbo: **POST**  
Política: **Administrator**  
Endpoint para cadastro de um desconto:

```css
{
    "idGame": 0,
    "startDiscount": "2025-06-01T23:48:49.077Z",
    "endDiscount": "2025-06-01T23:48:49.077Z",
    "discountPercentage": 0
}
```

Abaixo as regras dos campos para cadastrar novo desconto:

*   int - idGame - id do game que vai sofrer o desconto
*   datetime - startDiscount - data e horário em que o desconto se inicia
*   datetime - endDiscount - data e horário em que o desconto finaliza, deve ser superior a startDiscount
*   float - discountPercentage - indica a % de desconto no jogo. Ex. Para 50% de desconto deve ser 0.5. Para 67% de desconto, deve ser 0.67.

Todas as propriedades são obrigatórias.

###### /api/Administration/changeDiscountStatus

Verbo: **PUT**  
Política: **Administrator**  
Endpoint para alterar o status de um desconto (ativar ou desativar).

As informações para alterar o status são colocadas direto na URL por meio de parâmetro:

*   int - idDiscount - contêm o id do desconto que vai ter o status alterado
*   bool - newDiscountStatus - deve conter a informação (true) para deconto ativado, ou, (false) para desativado.

Ex.: Para desativar o desconto de id 3, esse trecho deve ser inserido ao fim do endpoint:  
**?idDiscount=3&newDiscountStatus=false**

###### /api/Administration/changeUserAccessLevel

Verbo: **PUT**  
Política: **Administrator**  
Endpoint para alterar a política de acesso de um usuário.

Assim como no endpoint anterior, as informações para alterar o status são colocadas direto na URL por meio de parâmetro:

*   int - idUser - contêm o id do usuário que vai ter a política alterada
*   int - idAccessLevel - id do novo access level que o usuário vai receber.

Inicialmente o sistema disponibiliza dois niveis de acesso.

*   Administrador - ID = 1
*   Usuário - ID = 2

Ex.: Para atribuir acessos de Administrador ao usuário de id 2, esse trecho deve ser inserido ao fim do endpoint:  
**?idUser=2&idAccessLevel=1**

---

#### AuthController

###### /api/Auth/getToken

Verbo: **POST**  
Política: Usuários cadastrados  
Esse endpoint permite autenticar o usuário e retorna o token Beare para utilização nas demais requisições.

O JSON deve estar estruturado da seguinte forma:

```css
{
    "email": "string",
    "password": "string"
}
```

Abaixo as regras dos campos para solicitação de token:

*   string - email - email do usuário
*   string - password - senha do usuário

###### /api/Auth/createUser

Verbo: **POST**  
Esse endpoint permite cadastrar novos usuários.

O JSON deve estar estruturado da seguinte forma:

```css
{
    "name": "string",
    "email": "string",
    "password": "string"
}
```

Abaixo as regras dos campos para solicitação de token:

*   string - name - nome do usuário
*   string - email - email do usuário
*   string - password - senha do usuário. Deve ter de 8 a 16 caracteres, uma letra minúscula, uma letra maiúscula, um número e um carater especial.

Todos os campos são obrigatórios

---

#### GamesController

###### /api/Games/getListGames

Verbo: **GET**  
Obtém uma lista dos jogos cadastrados e o seu desconto atual.

###### /api/Games/api/Games/addGameToLibrary

Verbo: **POST**  
Política: Usuários cadastrados  
Adiciona um jogo na biblioteca do usuário logado.

As informações para cadastro do joga na biblioteca são colocadas direto na URL por meio de parâmetro:

*   int - idGame - contêm o id do jogo que deve ser adicionado na biblioteca do usuário

Ex.: Para adicionar o jogo de id 3 na biblioteca do usuário, esse trecho deve ser inserido ao fim do endpoint:  
**?idGame=3**

###### /api/Games/getMyLibrary

Verbo: **GET**  
Política: Usuários cadastrados  
Obtém os jogos que pertencem a biblioteca do usuário logado.

---

## 📄 Autor

*   Nome: Daniel Cintra de Oliveira
*   Discord: @dco1993 (Daniel Cintra - RM356750)