resource "aws_iam_role" "ec2" {
  name = "example-ec2-role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Effect = "Allow",
        Principal = {
          Service = "ec2.amazonaws.com"
        },
        Action = "sts:AssumeRole"
      }
    ]
  })
}

# attached aws ec2 policy is attached

resource "aws_iam_role_policy_attachment" "ec2full_policy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2FullAccess"
  role       = aws_iam_role.ec2.name
}


# attached aws ecr policy is attached

resource "aws_iam_role_policy_attachment" "ec2_policy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2ContainerRegistryFullAccess"
  role       = aws_iam_role.ec2.name
  
}

# attached aws vpc policy is attached

resource "aws_iam_role_policy_attachment" "vpc_policy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonVPCFullAccess"
  role       =  aws_iam_role.ec2.name
  
  
  }

# attached aws iam policy is attached

resource "aws_iam_role_policy_attachment" "iam_policy" {
  policy_arn =  "arn:aws:iam::aws:policy/IAMFullAccess"
  role       =  aws_iam_role.ec2.name
 
  
  }
  
resource "aws_iam_instance_profile" "instance_profile" {
  name  = "jenkins_instance_profile"
  role = "${aws_iam_role.ec2.name}"
}
  
resource "aws_iam_role_policy_attachment" "task_role" {
  role       =  aws_iam_role.ec2.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}