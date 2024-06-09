pipeline {
    agent any
    environment {
		DOCKER_IMAGE = 'romanshmidt/kursova_site'
    }
    stages {
        stage('Start') {
            steps {
                echo 'Cursova_Site: nginx/custom'
            }
        }

        stage('Build Site') {
            steps {
                dir("Site_Frontend_Beckend")
				{
					sh 'docker-compose build'
				}
				sh 'docker tag webapp:latest $DOCKER_IMAGE:webapp-latest'
                sh 'docker tag webapp:latest $DOCKER_IMAGE:webapp-$BUILD_NUMBER'
				sh 'docker tag angular-app:latest $DOCKER_IMAGE:angular-app-latest'
                sh 'docker tag angular-app:latest $DOCKER_IMAGE:angular-app-$BUILD_NUMBER'

            }
            post{
                failure {
                    script {
                    // Send Telegram notification on success
                        telegramSend message: "Job Name: ${env.JOB_NAME}\n Branch: ${env.GIT_BRANCH}\nBuild #${env.BUILD_NUMBER}: ${currentBuild.currentResult}\n Failure stage: '${env.STAGE_NAME}'"
                    }
                }
            }
        }

        stage('Test Site service') {
            steps {
                echo 'Pass'
            }
            post{
                failure {
                    script {
                    // Send Telegram notification on success
                        telegramSend message: "Job Name: ${env.JOB_NAME}\nBranch: ${env.GIT_BRANCH}\nBuild #${env.BUILD_NUMBER}: ${currentBuild.currentResult}\nFailure stage: '${env.STAGE_NAME}'"
                    }
                }
            }
        }

		stage('Push to registry') {
            steps {
                withDockerRegistry([ credentialsId: "dockerhub_token", url: "" ])
                {
                    sh "docker push $DOCKER_IMAGE:webapp-latest"
                    sh "docker push $DOCKER_IMAGE:webapp-$BUILD_NUMBER"
					sh "docker push $DOCKER_IMAGE:angular-app-latest"
                    sh "docker push $DOCKER_IMAGE:angular-app-$BUILD_NUMBER"
                }
            }
            post{
                failure {
                    script {
                    // Send Telegram notification on success
                        telegramSend message: "Job Name: ${env.JOB_NAME}\nBranch: ${env.GIT_BRANCH}\nBuild #${env.BUILD_NUMBER}: ${currentBuild.currentResult}\nFailure stage: '${env.STAGE_NAME}'"
                    }
                }
            }
        }

        stage('Deploy Site services') {
            steps {
				dir("Site_Frontend_Beckend"){
					sh "docker-compose down -v"
                	sh "docker container prune --force"
                	sh "docker image prune --force"
                	sh "docker-compose up -d --build"
				}
            }
            post{
                failure {
                    script {
                    // Send Telegram notification on success
                        telegramSend message: "Job Name: ${env.JOB_NAME}\nBranch: ${env.GIT_BRANCH}\nBuild #${env.BUILD_NUMBER}: ${currentBuild.currentResult}\nFailure stage: '${env.STAGE_NAME}'"
                    }
                }
            }
        }
    }

    post {
        success {
            script {
                // Send Telegram notification on success
                telegramSend message: "Job Name: ${env.JOB_NAME}\n Branch: ${env.GIT_BRANCH}\nBuild #${env.BUILD_NUMBER}: ${currentBuild.currentResult}"
            }
        }
    }
}
