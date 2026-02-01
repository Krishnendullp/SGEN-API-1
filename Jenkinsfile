pipeline {
  agent any

  stages {
    stage('Git Pull') {
      steps {
        sh 'git pull origin main'
      }
    }

    stage('Docker Build') {
      steps {
        sh 'docker build -t myapi .'
      }
    }

    stage('Docker Deploy') {
      steps {
        sh '''
        docker stop myapi_container || true
        docker rm myapi_container || true
        docker run -d -p 7072:7072 --name myapi_container myapi
        '''
      }
    }
  }
}
