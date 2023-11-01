resource "aws_ecr_repository" "ecr" {
    name = "${local.env}-ecr"
    image_tag_mutability = "MUTABLE"
    

    image_scanning_configuration {
      scan_on_push = true
    }
    
    tags = {
      Name = "${local.env}-ecr"
    }
}