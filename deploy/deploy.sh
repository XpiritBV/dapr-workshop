kubectl config set-context --namespace dapr-demoapp

#kubectl proxy 
#http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/#/overview?namespace=default

############
## DAPR
############

#https://raw.githubusercontent.com/dapr/dashboard/master/deploy/dashboard.yaml
kubectl apply -f ./deploy/dapr/dashboard.yml -n dapr

############
## DAPR-DEMOAPP
############

kubectl apply -f ./deploy/dapr-demoapp/shippingcostsservice.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/loyaltyservice.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/orderapi.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/ordergenerator.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/shippingservice.yml -n dapr-demoapp

kubectl delete --all pods --namespace=dapr-demoapp

############
## STATE-DEMO
############

kubectl apply -f ./deploy/dapr-demoapp/dapr/state-redis.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/dapr/state-cosmosdb.yml -n dapr-demoapp

############
## TRACING-DEMO
############

kubectl apply -f ./deploy/dapr-demoapp/dapr/dapr-tracing-exporterAzure.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/dapr/dapr-tracing-exporterjaegerzipkin.yml -n dapr-demoapp
kubectl apply -f ./deploy/dapr-demoapp/dapr/dapr-tracing-exporterzipkin.yml -n dapr-demoapp

kubectl apply -f ./deploy/tracing-demo/ai-localforwarder.yml -n tracing-demo

kubectl apply -f ./deploy/tracing-demo/zipkin-deployment.yml -n tracing-demo

#deploy jaeger operator
#https://github.com/jaegertracing/jaeger-operator 

kubectl create namespace observability
kubectl create -n observability -f https://raw.githubusercontent.com/jaegertracing/jaeger-operator/master/deploy/crds/jaegertracing.io_jaegers_crd.yaml
kubectl create -n observability -f https://raw.githubusercontent.com/jaegertracing/jaeger-operator/master/deploy/service_account.yaml
kubectl create -n observability -f https://raw.githubusercontent.com/jaegertracing/jaeger-operator/master/deploy/role.yaml
kubectl create -n observability -f https://raw.githubusercontent.com/jaegertracing/jaeger-operator/master/deploy/role_binding.yaml
kubectl create -n observability -f https://raw.githubusercontent.com/jaegertracing/jaeger-operator/master/deploy/operator.yaml

kubectl apply -f ./deploy/tracing-demo/jaegersimplest.yml -n observability


############
## PUBSUB-DEMO
############

kubectl apply -f ./deploy/dapr-demoapp/dapr/pubsub-redis.yml -n dapr-demoapp
