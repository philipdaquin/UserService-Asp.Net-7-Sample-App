resource "aws_ecs_task_definition" "ecs_task_definition" {
    family             = "my-ecs-task"
    network_mode       = "awsvpc"
    execution_role_arn = aws_iam_role.ec2.arn
    task_role_arn =  aws_iam_role.ec2.arn
    cpu                = 256


    runtime_platform {
        operating_system_family = "LINUX"
        cpu_architecture        = "X86_64"
    }

    container_definitions = jsonencode([
        {
            name      = "${local.env}-ecs"
            image     = "${aws_ecr_repository.ecr.repository_url}:latest"
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

