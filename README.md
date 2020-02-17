# Xpirit Dapr Workshop & Demos

## Setup

- install helm 3 (https://helm.sh/docs/intro/install/)
- enable Kubernetes in Docker For Windows (https://www.docker.com/blog/docker-windows-desktop-now-kubernetes/)

- select the right Kubernetes environment

```bash 
kubectl config get-contexts
kubectl config use-context docker-for-desktop
```

in WSL connect the kube config file to the kube config on windows so you can access Kubernetes running in Docker for Windows.
```bash
mkdir -p ~/.kube
ln -sf /c/users/<YOUR_USER>/.kube/config ~/.kube/config
```

installing Dapr

https://github.com/dapr/docs/blob/master/getting-started/environment-setup.md#installing-dapr-on-a-kubernetes-cluster
