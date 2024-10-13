# Tech Challenge

## Como executar o container com o projeto, prometheus e grafana

Dentro da pasta do projeto `tech-challenge-01/src` existe um docker-compose.yml que deve ser executado.

Para rodar o projeto pela primeira vez, basta rodar os seguintes comandos:

```shell
docker compose build
docker compose up
```

Após isso as próximas vezes basta rodar o comando:
```shell
docker compose up
```

Assim o container com os três estará rodando perfeitamente.
Para acessar a cada um deles, segue a lista de urls:

- http://localhost:8080/swagger/index.html
- http://localhost:8080/metrics (Dados que o prometheus está gerando)
- http://localhost:9090/targets?search= (Prometheus interface)
- http://localhost:3000/dashboard/new?orgId=1&editPanel=1 (Grafana)

já está tudo funcionando bem.

Segui este artigo, caso necessita verificar algo:

[Artigo de como usar o prometheus e o grafana no aspnet](https://medium.com/@faulycoelho/net-8-api-with-prometheus-and-grafana-29003adafd43)
