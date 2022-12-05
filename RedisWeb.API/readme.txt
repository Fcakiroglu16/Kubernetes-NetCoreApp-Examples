helm install redis bitnami/redis
helm list
helm delete redis

- to access from localhost
kubectl port-forward --namespace default svc/redis-master 6380:6379

- list to port-forwards
ps -ef|grep port-forward
- remove specific port-forward
kill -9 2013886

- apply yaml file
kubectl apply -f service-and-deployment.yaml


- to send to docker-hub
docker-compose -f docker-compose.yml -f docker-compose.override.yml push


- to tunnel for access from browser
minikube service svc-redis-api







