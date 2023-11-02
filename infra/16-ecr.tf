resource "aws_ecr_repository" "ecr" {
    name = "${local.env}-ecr"
    image_tag_mutability = "MUTABLE"
    
    force_delete = true

    image_scanning_configuration {
      scan_on_push = true
    }
    
    tags = {
      Name = "${local.env}-ecr"
    }
}