#SETUP dapr tracing

kubectl apply -f ./dist-tracing/dapr-tracing.yaml

# Dapr Tracing to Application Insights
kubectl apply -f ./dist-tracing/localforwarder-deployment.yaml 
kubectl apply -f ./dist-tracing/dapr-tracing-exporterAzure.yaml 

# Dapr Tracing to Zipkin

# Dapr Tracing to Jaeger

kubectl proxy 
http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/#/overview?namespace=default


#install dashboard
kubectl apply -f https://raw.githubusercontent.com/dapr/dashboard/master/deploy/dashboard.yaml