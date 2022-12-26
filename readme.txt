- to install helm
https://medium.com/@JockDaRock/take-the-helm-with-kubernetes-on-windows-c2cd4373104b


- link
https://bitnami.com/stack/kafka/helm
- to install kafka
helm install my-release bitnami/kafka

- documentation for c#
https://docs.confluent.io/kafka-clients/dotnet/current/overview.html

- step by step for c#
https://developer.confluent.io/get-started/dotnet/

- port-forward
kubectl port-forward services/my-release-kafka 9092:9092


- to run bash scripts
/opt/bitnami/kafka/bin

- create topic
@my-release-kafka-0:/opt/bitnami/kafka/bin$ kafka-topics.sh --create --bootstrap-server localhost:9092 --topic hello_world --partitions 3 --replication-factor 1

- list topic
kafka-topics.sh --list --bootstrap-server  localhost:9092

- describe topic
kafka-topics.sh --bootstrap-server localhost:9092 --describe --topic hello_world


- describe pod
kubectl get pods my-release-kafka-0


- run bash 
kubectl exec --stdin --tty my-release-kafka-0  -- /bin/bash

- produce event
kafka-console-producer.sh --bootstrap-server localhost:9092 --topic hello_world

-consume event
kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic hello_world --from-beginning

