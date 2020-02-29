kubectl config set-context --current --namespace= dapr-demoapp

##State store
kubectl apply -f ./deploy/redis.yml -n dapr-demoapp

#SETUP dapr tracing

kubectl apply -f ./deploy/dist-tracing/dapr-tracing.yaml -n dapr-demoapp

# Dapr Tracing to Application Insights
kubectl apply -f ./deploy/dist-tracing/localforwarder-deployment.yaml -n dapr-demoapp
kubectl apply -f ./deploy/dist-tracing/dapr-tracing-exporterAzure.yaml  -n dapr-demoapp

# Dapr Tracing to Zipkin
kubectl apply -f ./deploy/dist-tracing/dapr-tracing-exporterzipkin.yaml -n dapr-demoapp
# Dapr Tracing to Jaeger

kubectl proxy 
http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/#/overview?namespace=default


#install dashboard
kubectl apply -f https://raw.githubusercontent.com/dapr/dashboard/master/deploy/dashboard.yaml




#install all pods

kubectl apply -f ./deploy/shippingcostsservice.yml -n dapr-demoapp
kubectl apply -f ./deploy/loyaltyservice.yml -n dapr-demoapp
kubectl apply -f ./deploy/orderapi.yml -n dapr-demoapp
kubectl apply -f ./deploy/ordergenerator.yml -n dapr-demoapp