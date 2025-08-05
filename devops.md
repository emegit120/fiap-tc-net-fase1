# FIAP Cloud Games (FCG) - DevOps

O processo de deploy do **FIAP Cloud Games (FCG)** é através da cloud Azure Devops.

---

## Dockerfile

Configurado para realizar o build e deploy da aplicação em cloud.

## Branchs

- development -> Ambiente de desenvolvimento (validação de testes e QA)
- release -> Ambiente de Stage (validação dos stakeholders)
- master -> Ambiente de Produção

## Workflow

- **Features:** Para realização de novas features realizar uma cópia da branch release e abrir um 'Pull Request' para development. (feature/XXX)
- **Hotfix:** Para realização de novas features realizar uma cópia da branch master e abrir o 'Pull Request' para master. (hotfix/XXX)
---

## Pipeline multistage

Os triggers foram configurados para serem disparados quando receberem commits e merges nas branchs correspondentes.

trigger:
  branches:
    include:
      - master
      - release
      - development

OBS: para deploys em stage(release) e produção(master) será necessário uma aprovação para o deploy prosseguir.

O arquivo azure-pipelines-1.yml esta descrito todo o processo de build e deploy. Todos os deploys criarão uma imagem docker que será enviada para o Azure Container Registry acremetechchallenge. O Azure Container Apps automaticamente carregará a nova imagem com a tag de build (BuildId).

`- task: Docker@2
      displayName: Build and push an image to container registry
      inputs:
        containerRegistry: 'acremetechchallenge'
        repository: 'pipeline-acr'
        command: 'buildAndPush'
      Dockerfile: '**/Dockerfile'`

**URL web:** 
https://appcontainer-fiap.bluewave-37b6d784.eastus.azurecontainerapps.io/swagger/index.html


## Azure Container Apps

Após o carregamento da imagem do container o Azure Container Apps está configurado para uma escala de manter no mínimo 2 réplicas ativas e 3 no máximo, cada réplica esta configurada para aceitar 2 requisições simultâneas, assim podemos analisar a escalabilidade e resiliência da aplicação.


## Monitoramento e observalidade (New Relic)

As métricas e logs da aplicação são enviadas ao painel do New Relic, temos alertas configurados para erros e Throughputs. Assim podemos verificar issues e health check da nossa aplicação.


## Entregáveis da Fase 2

- Dockerização com dockerfile
- Escalabilidade e resiliência com Azure Container Apps.
- Pipeline multistage CI/CD.
- Publicação na cloud Azure DevOps.
- Monitoramento e gerenciamento de logs com New Relic.
- devops.md - documentação (este arquivo).
- Relatório de entrega com links do grupo, documentação, repositório e vídeo.

---

 Nome do grupo
 - Grupo 81

○ Participantes e usernames no Discord.
 - emerson1446 
 - Emerson (Dev)

Link da documentação.
https://www.figma.com/board/1s1MvlkKbtpnq9UmfFQfv3/DDD---Event-Storming?node-id=0-1&p=f

GITHUB
https://github.com/emegit120/fiap-tc-net-fase1

Video
https://drive.google.com/drive/folders/1aWhEohEN1jrx0Us3lN1ivedb1SbVVxTo?usp=sharing


## Licença

Este projeto é apenas para fins educacionais no contexto do Tech Challenge FIAP.
