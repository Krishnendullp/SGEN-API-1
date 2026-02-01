pipeline {
    agent any

    environment {
        IMAGE = "sgen-billing-api:latest"
        GREEN = "sgen-billing-api-green"
        BLUE  = "sgen-billing-api-blue"
        GREEN_PORT = "7071"
        BLUE_PORT  = "7072"
    }

    stages {

        stage('Checkout from Git') {
            steps {
                checkout scm
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t $IMAGE .'
            }
        }

        stage('Run GREEN (Safe port)') {
            steps {
                sh '''
                docker rm -f $GREEN || true
                docker run -d --name $GREEN -p ${GREEN_PORT}:8080 $IMAGE
                '''
            }
        }

        stage('Health Check') {
            steps {
                sh '''
                for i in {1..20}; do
                  echo "Checking port ${GREEN_PORT} (attempt $i)"
                  if command -v nc >/dev/null 2>&1; then
                    nc -z localhost ${GREEN_PORT} && echo "Port is open, API is up" && exit 0
                  else
                    curl -s http://localhost:${GREEN_PORT}/swagger/index.html && echo "API is up" && exit 0
                  fi
                  sleep 5
                done
                echo "API did not start on port ${GREEN_PORT}"
                exit 1
                '''
            }
        }

        stage('Promote to BLUE') {
            steps {
                sh '''
                docker rm -f $BLUE || true
                docker run -d --name $BLUE -p ${BLUE_PORT}:8080 $IMAGE
                docker rm -f $GREEN || true
                '''
            }
        }
    }

    post {
        always {
            sh 'docker ps -a'
        }
        success {
            echo "Deployment Successful ✅"
        }
        failure {
            echo "Deployment Failed ❌"
        }
    }
}
