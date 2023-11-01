resource "aws_autoscaling_group" "ecs_asg" {
    vpc_zone_identifier = [aws_subnet.public_zone_1.id, aws_subnet.public_zone_2.id]
    desired_capacity    = 2
    max_size            = 3
    min_size            = 1

    launch_template {
        id      = aws_launch_template.ecs_lt.id
        version = "$Latest"
    }

    tag {
        key                 = "AmazonECSManaged"
        value               = true
        propagate_at_launch = true
    }
}