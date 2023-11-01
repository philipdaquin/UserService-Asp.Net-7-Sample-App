
provider "aws" {
    region = local.region
}

terraform {
    required_version = ">=1.0"

    required_providers {
        aws = { 
            source = "hashicorp/aws"
            version = "~>5.16"
        }
        docker = {
            source  = "kreuzwerker/docker"
            version = "~>2.20.0"
        }
    }
}