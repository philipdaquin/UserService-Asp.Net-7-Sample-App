resource "aws_ecs_service" "ecs_service" {
    name            = "my-ecs-service"
    cluster         = aws_ecs_cluster.ecs_cluster.id
    task_definition = aws_ecs_task_definition.ecs_task_definition.arn
    desired_count   = 2

    network_configuration {
        subnets         = [aws_subnet.public_zone_1.id, aws_subnet.public_zone_2.id]
        security_groups = [aws_security_group.sg.id]
    }

    force_new_deployment = true

    placement_constraints {
        type = "distinctInstance"
    }

    triggers = {
        redeployment = timestamp()
    }

    capacity_provider_strategy {
        capacity_provider = aws_ecs_capacity_provider.ecs_capacity_provider.name
        weight            = 100
    }

    load_balancer {
        target_group_arn = aws_lb_target_group.ecs_tg.arn
        container_name   = "dockergs"
        container_port   = 80
    }

    depends_on = [aws_autoscaling_group.ecs_asg]
}