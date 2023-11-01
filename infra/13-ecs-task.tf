resource "aws_ecs_task_definition" "ecs_task_definition" {
    family             = "my-ecs-task"
    network_mode       = "awsvpc"
    execution_role_arn = "arn:aws:iam::532199187081:role/ecsTaskExecutionRole"
    cpu                = 256


    runtime_platform {
        operating_system_family = "LINUX"
        cpu_architecture        = "X86_64"
    }

    container_definitions = jsonencode([
        {
            name      = "dockergs"
            image     = "public.ecr.aws/f9n5f1l7/dgs:latest"
            cpu       = 256
            memory    = 512
            essential = true
            portMappings = [
            {
                containerPort = 80
                hostPort      = 80
                protocol      = "tcp"
            }
            ]
        }
    ])
}