resource "aws_subnet" "private_zone_1" {
    vpc_id            = aws_vpc.main.id
    cidr_block        = "10.2.0.0/19"
    availability_zone = local.zone1
    
    tags = {
        "Name"                                                 = "${local.env}-private-${local.zone1}"
    }
}       

resource "aws_subnet" "private_zone_2" {
    vpc_id            = aws_vpc.main.id
    cidr_block        = "10.2.32.0/19"
    availability_zone = local.zone2
    
    tags = {
        "Name"                                                 = "${local.env}-private-${local.zone1}"
    }
}       

resource "aws_subnet" "public_zone_1" {
    vpc_id            = aws_vpc.main.id
    cidr_block        = "10.2.64.0/19"
    availability_zone = local.zone1
    map_public_ip_on_launch = true
    
    tags = {
        "Name"                                                 = "${local.env}-public-${local.zone1}"
    }
}       

resource "aws_subnet" "public_zone_2" {
    vpc_id            = aws_vpc.main.id
    cidr_block        = "10.2.96.0/19"
    availability_zone = local.zone2
    map_public_ip_on_launch = true
    
    tags = {
        "Name"                                                 = "${local.env}-public-${local.zone1}"
    }
}       