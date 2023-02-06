#! /bin/sh

docker compose -f docker-ci.yml up
RETURN_QUALINTY=$? 

if [ $RETURN_QUALINTY -eq 0 ]
then
    echo 'Quality stage - passed!'

    docker compose -f ./tests-in-docker.yml up --exit-code-from integration_tests -- integration_tests
    RETURN_INTEGRATION=$?

    if [ $RETURN_INTEGRATION -eq 0 ]
    then
        echo 'Integration Tests stage - passed!'

        docker compose -f ./tests-in-docker.yml up --exit-code-from functional_tests -- functional_tests
        RETURN_FUNCTIONAL=$?

        if [ $RETURN_FUNCTIONAL -eq 0 ]
        then
            echo 'Functional Tests stage - passed!'
        else
            echo 'Functional Tests stage - failed!'
            exit 1
        fi

    else
        echo 'Integration Tests  stage- failed!'
        exit 1
    fi
else
    echo 'Quality stage - failed!'
    exit 1
fi

exit 0