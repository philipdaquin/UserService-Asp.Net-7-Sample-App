resource "aws_launch_template" "ecs_lt" {
  name_prefix   = "ecs-template"
  image_id      = data.aws_ami.ubuntu.image_id
  instance_type = local.instance_type
  key_name               = local.key_name
  vpc_security_group_ids = [aws_security_group.sg.id]
  update_default_version = true
  user_data = filebase64("${path.module}/ecs.sh")

  
  iam_instance_profile {
    name = aws_iam_instance_profile.instance_profile.name
  }

  block_device_mappings {
    device_name = "/dev/xvda"
    ebs {
      volume_size = 30
      volume_type = "gp2"
    }
  }

  tag_specifications {
    resource_type = "instance"
    tags = {
      Name = "ecs-instance"
    }
  }

}