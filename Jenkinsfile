pipeline {
    agent any

    environment {
        IMAGE = "sgen-billing-api:latest"
        GREEN = "sgen-billing-api-green"
        BLUE  = "sgen-billing-api-blue"
        GREEN_PORT = "7001"
        BLUE_PORT  = "7002"
    }

    stages {

        stage('Checkout from Git') {
            steps {
                checkout scm
            }
        }

        stage('Build Docker Image (Solution Root)') {
            steps {
                sh 'docker build -t $IMAGE .'
            }
        }

        stage('Run GREEN (safe port)') {
            steps {
                sh '''
                docker rm -f $GREEN || true
                docker run -d --name $GREEN -p ${GREEN_PORT}:8080 $IMAGE
                '''
            }
        }

        stage('Health Check (PORT ONLY, bash native)') {
            steps {
                sh '''
                for i in {1..20}; do
                  echo "Checking port ${GREEN_PORT} (attempt $i)"
                  if (echo > /dev/tcp/127.0.0.1/${GREEN_PORT}) >/dev/null 2>&1; then
                    echo "Port is open, API is up"
                    exit 0
                  fi
                  sleep 5
                done
                echo "API did not start on port ${GREEN_PORT}"
                exit 1
                '''
            }
        }

        stage('Promote to BLUE (safe port)') {
            steps {
                sh '''
                docker rm -f $BLUE || true
                docker run -d --name $BLUE -p ${BLUE_PORT}:8080 $IMAGE
                docker rm -f $GREEN
                '''
            }
        }
    }
}
