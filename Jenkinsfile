pipeline {
    agent any

    environment {
        IMAGE = "sgen-billing-api:latest"
        GREEN = "sgen-billing-api-green"
        BLUE  = "sgen-billing-api-blue"
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build API Docker Image') {
            steps {
                sh 'docker build -t $IMAGE .'
            }
        }

        stage('Run GREEN (7001)') {
            steps {
                sh '''
                docker rm -f $GREEN || true
                docker run -d --name $GREEN -p 7001:8080 $IMAGE
                '''
            }
        }

        stage('Health Check') {
            steps {
                sh '''
                sleep 15
                curl -f http://localhost:7001/health
                '''
            }
        }

        stage('Promote to BLUE (7002)') {
            steps {
                sh '''
                docker rm -f $BLUE || true
                docker run -d --name $BLUE -p 7002:8080 $IMAGE
                docker rm -f $GREEN
                '''
            }
        }
    }
}
