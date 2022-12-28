
- send to docker-hub
docker-compose build
docker-compose -f docker-compose.yml push
- create kubernetes service object
kubectl apply -f service-deployment-kafka.yaml
- tunnel for service object
minikube service <producer-service-name>
- install bitnami/kafka with helm chart
https://bitnami.com/stack/kafka/helm
